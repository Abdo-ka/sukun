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
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://api.quran.com/api/v4/");

                // خريطة المصادر إلى slug في Quran.com API (بناءً على المصادر المتاحة)
                var sourceMap = new Dictionary<TafsirSource, (string Slug, string Author)>
            {
                { TafsirSource.IbnKathir, ("tafsir-ibnkathir", "تفسير ابن كثير") },
                { TafsirSource.Jalalayn, ("ar.jalalayn", "تفسير الجلالين") },
                { TafsirSource.Qurtubi, ("ar.tafsir-qurtubi", "تفسير القرطبي") }, // إذا متاح
                { TafsirSource.Tabari, ("ar.tafsir-tabari", "تفسير الطبري") },
                { TafsirSource.Saadi, ("ar.tafsir-saadi", "تفسير السعدي") },
                { TafsirSource.Modern, ("ar.muyassar", "التفسير الميسر") } // حديث
            };

                if (!sourceMap.TryGetValue(source, out var tafsirInfo))
                {
                    _logger.LogError("Tafsir source {Source} not supported", source);
                    return;
                }

                var (slug, author) = tafsirInfo;

                // جلب قائمة التفاسير للحصول على ID
                var tafsirsResponse = await client.GetFromJsonAsync<TafsirsListResponse>("resources/tafsirs");
                var tafsirResource = tafsirsResponse?.Tafsirs.FirstOrDefault(t => t.Slug == slug);
                if (tafsirResource == null)
                {
                    _logger.LogError("Tafsir resource '{Slug}' not found in API", slug);
                    return;
                }

                var tafsirId = tafsirResource.Id;

                // جلب التفسير لكل سورة (1-114)
                for (int surahNumber = 1; surahNumber <= 114; surahNumber++)
                {
                    var url = $"tafsirs/{tafsirId}?chapter_number={surahNumber}";
                    var response = await client.GetFromJsonAsync<TafsirApiResponse>(url);

                    if (response?.Tafsirs == null || !response.Tafsirs.Any())
                    {
                        _logger.LogWarning("No tafsir data for Surah {SurahNumber}", surahNumber);
                        continue;
                    }

                    foreach (var apiTafsir in response.Tafsirs)
                    {
                        // استخراج رقم السورة ورقم الآية من verse_key مثل "2:255"
                        var parts = apiTafsir.VerseKey.Split(':');
                        if (parts.Length != 2 || !int.TryParse(parts[0], out int surah) || !int.TryParse(parts[1], out int verseNum))
                            continue;

                        // جلب الآية المحلية
                        var verse = await _unitOfWork.Repository<QuranVerse>()
                            .FirstOrDefaultAsync(v => v.Surah.Number == surah && v.VerseNumber == verseNum);

                        if (verse == null)
                        {
                            _logger.LogWarning("Verse {VerseKey} not found in local database", apiTafsir.VerseKey);
                            continue;
                        }

                        // تحقق من عدم التكرار
                        var existing = await _unitOfWork.Repository<Tafsir>()
                            .FirstOrDefaultAsync(t => t.VerseId == verse.Id && t.Source == source);

                        if (existing != null) continue;

                        var tafsir = new Tafsir
                        {
                            VerseId = verse.Id,
                            Source = source,
                            Author = author,
                            Text = apiTafsir.Text.Trim()
                        };

                        await _unitOfWork.Repository<Tafsir>().AddAsync(tafsir);
                    }

                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation("Seeded {Source} tafsir for Surah {SurahNumber}", source, surahNumber);
                }

                _logger.LogInformation("Tafsir seeding completed for source: {Source}", source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during tafsir seeding for source {Source}", source);
                throw;
            }
        }
    }
}
