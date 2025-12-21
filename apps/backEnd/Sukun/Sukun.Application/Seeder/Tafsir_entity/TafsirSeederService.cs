using Microsoft.Extensions.Logging;
using Sukun.Application.Seeder.Tafsir_entity.Dto;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Net.Http.Json;

namespace Sukun.Application.Seeder.Tafsir_entity
{
    public class TafsirSeederService : ITafsirSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TafsirSeederService> _logger;

        public TafsirSeederService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, ILogger<TafsirSeederService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SeedTafsirAsync(TafsirSource source = TafsirSource.Jalalayn)
        {
            try
            {
                var tafsirRepo = _unitOfWork.Repository<Tafsir>();

                // تحقق داخلي: إذا كان التفسير موجود لأي آية → افترض أنه كامل (أو تحقق بعدد الآيات ≈6236)
                var existingCount = await tafsirRepo.CountAsync(t => t.Source == source);
                if (existingCount > 5000) // تقريبي، لأن عدد الآيات 6236
                {
                    _logger.LogInformation("Tafsir {Source} already seeded ({Count} entries), skipping.", source, existingCount);
                    return;
                }

                _logger.LogInformation("Starting seeding for Tafsir {Source}...", source);

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://api.quran.com/api/v4/");

                var sourceMap = new Dictionary<TafsirSource, (string Slug, string Author)>
            {
                { TafsirSource.IbnKathir, ("tafsir-ibnkathir", "تفسير ابن كثير") },
                { TafsirSource.Jalalayn, ("ar.jalalayn", "تفسير الجلالين") },
                { TafsirSource.Qurtubi, ("ar.tafsir-qurtubi", "تفسير القرطبي") },
                { TafsirSource.Tabari, ("ar.tafsir-tabari", "تفسير الطبري") },
                { TafsirSource.Saadi, ("ar.tafsir-saadi", "تفسير السعدي") },
                { TafsirSource.Modern, ("ar.muyassar", "التفسير الميسر") }
            };

                if (!sourceMap.TryGetValue(source, out var tafsirInfo))
                {
                    _logger.LogError("Tafsir source {Source} not supported", source);
                    return;
                }

                var (slug, author) = tafsirInfo;

                var tafsirsResponse = await client.GetFromJsonAsync<TafsirsListResponse>("resources/tafsirs");
                var tafsirResource = tafsirsResponse?.Tafsirs.FirstOrDefault(t => t.Slug == slug);
                if (tafsirResource == null)
                {
                    _logger.LogError("Tafsir resource '{Slug}' not found in API", slug);
                    return;
                }

                var tafsirId = tafsirResource.Id;
                int addedCount = 0;

                for (int surahNumber = 1; surahNumber <= 114; surahNumber++)
                {
                    var url = $"tafsirs/{tafsirId}?chapter_number={surahNumber}";
                    var response = await client.GetFromJsonAsync<TafsirApiResponse>(url);

                    if (response?.Tafsirs == null || !response.Tafsirs.Any()) continue;

                    foreach (var apiTafsir in response.Tafsirs)
                    {
                        var parts = apiTafsir.VerseKey.Split(':');
                        if (parts.Length != 2 || !int.TryParse(parts[0], out int surah) || !int.TryParse(parts[1], out int verseNum)) continue;

                        var verse = await _unitOfWork.Repository<QuranVerse>()
                            .FirstOrDefaultAsync(v => v.Surah.Number == surah && v.VerseNumber == verseNum);

                        if (verse == null) continue;

                        var existing = await tafsirRepo.FirstOrDefaultAsync(t => t.VerseId == verse.Id && t.Source == source);
                        if (existing != null) continue;

                        var tafsir = new Tafsir
                        {
                            VerseId = verse.Id,
                            Source = source,
                            Author = author,
                            Text = apiTafsir.Text.Trim()
                        };

                        await tafsirRepo.AddAsync(tafsir);
                        addedCount++;
                    }

                    if (addedCount > 0 && addedCount % 1000 == 0) // حفظ كل 1000 آية تقريبًا لتحسين الأداء
                    {
                        await _unitOfWork.CompleteAsync();
                    }
                }

                if (addedCount > 0)
                {
                    await _unitOfWork.CompleteAsync();
                }

                _logger.LogInformation("Tafsir {Source} seeding completed. Added {AddedCount} entries.", source, addedCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during tafsir seeding for source {Source}", source);
                throw;
            }
        }
    }
}
