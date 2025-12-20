using Microsoft.Extensions.Logging;
using Sukun.Application.Seeder.Quran.dto;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Net.Http.Json;

namespace Sukun.Application.Seeder.Quran
{
    public class QuranSeederService : IQuranSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<QuranSeederService> _logger;

        public QuranSeederService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, ILogger<QuranSeederService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SeedQuranAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://api.alquran.cloud/v1/");

                var surahsResponse = await client.GetFromJsonAsync<QuranApiResponse>("quran/quran-uthmani");
                if (surahsResponse == null || surahsResponse.Data?.Surahs == null)
                {
                    _logger.LogError("Failed to fetch Quran data from API");
                    return;
                }

                foreach (var apiSurah in surahsResponse.Data.Surahs)
                {
                    var surah = new QuranSurah
                    {
                        Number = apiSurah.Number,
                        Name = apiSurah.Name,
                        EnglishName = apiSurah.EnglishName,
                        //EnglishNameTranslation = apiSurah.EnglishNameTranslation,
                        TotalVerses = apiSurah.Ayahs.Count,
                        RevelationType = apiSurah.RevelationType == "Meccan" ? RevelationType.Makki : RevelationType.Madani,
                        RevelationOrder = apiSurah.RevelationOrder
                    };

                    foreach (var apiAyah in apiSurah.Ayahs)
                    {
                        var verse = new QuranVerse
                        {
                            VerseNumber = apiAyah.NumberInSurah,
                            Text = apiAyah.Text,
                            PageNumber = apiAyah.Page,
                            JuzNumber = apiAyah.Juz,
                            HizbNumber = apiAyah.HizbQuarter, // أو حسب الحاجة
                            Surah = surah
                        };

                        surah.Verses.Add(verse);
                    }

                    // تحقق إذا السورة موجودة مسبقًا (لتجنب التكرار)
                    var existingSurah = await _unitOfWork.Repository<QuranSurah>()
                        .FirstOrDefaultAsync(s => s.Number == surah.Number);

                    if (existingSurah == null)
                    {
                        await _unitOfWork.Repository<QuranSurah>().AddAsync(surah);
                    }
                }

                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Quran seeded successfully with {Count} surahs", surahsResponse.Data.Surahs.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding Quran data");
                throw;
            }
        }
    }
}
