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
                var surahRepo = _unitOfWork.Repository<QuranSurah>();
                var existingSurahsCount = await surahRepo.CountAsync();
                if (existingSurahsCount >= 114)
                {
                    _logger.LogInformation("Quran already fully seeded ({Count}/114 surahs), skipping.", existingSurahsCount);
                    return;
                }

                _logger.LogInformation("Starting Quran seeding... Current surahs: {Count}/114", existingSurahsCount);

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri("https://api.alquran.cloud/v1/");

                var response = await client.GetFromJsonAsync<QuranApiResponse>("quran/quran-uthmani");

                if (response == null || response.Data?.Surahs == null)
                {
                    _logger.LogError("Failed to fetch Quran data from API");
                    return;
                }

                int addedSurahs = 0;
                foreach (var apiSurah in response.Data.Surahs)
                {
                    var existingSurah = await surahRepo.FirstOrDefaultAsync(s => s.Number == apiSurah.Number);
                    if (existingSurah != null)
                    {
                        _logger.LogDebug("Surah {Number} already exists, skipping.", apiSurah.Number);
                        continue;
                    }

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
                            HizbNumber = apiAyah.HizbQuarter,
                            Surah = surah
                        };
                        surah.Verses.Add(verse);
                    }

                    await surahRepo.AddAsync(surah);
                    addedSurahs++;
                }

                if (addedSurahs > 0)
                {
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation("Quran seeding completed. Added {AddedCount} surahs.", addedSurahs);
                }
                else
                {
                    _logger.LogInformation("No new surahs to add.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding Quran data");
                throw;
            }
        }
    }
}
