using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sukun.Application.Seeder.AsumalHausna_entity
{
    public class AsmaulHusnaSeederService : IAsmaulHusnaSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AsmaulHusnaSeederService> _logger;

        public AsmaulHusnaSeederService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, ILogger<AsmaulHusnaSeederService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SeedAsmaulHusnaAsync()
        {
            try
            {
                var repo = _unitOfWork.Repository<AsmaulHusna>();

                // تحقق داخلي: إذا كانت 99 اسمًا موجودة → لا تفعل شيء
                var existingCount = await repo.CountAsync();
                if (existingCount >= 99)
                {
                    _logger.LogInformation("Asmaul Husna already fully seeded ({Count}/99 names), skipping.", existingCount);
                    return;
                }

                _logger.LogInformation("Starting Asmaul Husna seeding... Current: {Count}/99", existingCount);

                var client = _httpClientFactory.CreateClient();
                var url = "https://cdn.jsdelivr.net/gh/fawazahmed0/quran-api@1/others/names.json";

                var json = await client.GetStringAsync(url);
                var apiNames = JsonSerializer.Deserialize<List<ApiAsmaulHusna>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiNames == null || apiNames.Count == 0)
                {
                    _logger.LogError("Failed to fetch Asmaul Husna data from CDN");
                    return;
                }

                int addedCount = 0;
                foreach (var apiName in apiNames)
                {
                    var existing = await repo.FirstOrDefaultAsync(a => a.Number == apiName.Number);
                    if (existing != null) continue;

                    var name = new AsmaulHusna
                    {
                        Number = apiName.Number,
                        NameArabic = apiName.Name?.Trim() ?? "غير معروف",
                        NameTransliteration = apiName.Transliteration?.Trim() ?? "",
                        MeaningArabic = apiName.MeaningAr?.Trim() ?? "",
                        MeaningEnglish = apiName.MeaningEn?.Trim() ?? ""
                    };

                    await repo.AddAsync(name);
                    addedCount++;
                }

                if (addedCount > 0)
                {
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation("Asmaul Husna seeding completed. Added {AddedCount} names.", addedCount);
                }
                else
                {
                    _logger.LogInformation("No new Asmaul Husna names to add.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Asmaul Husna seeding");
                throw;
            }
        }
        public async Task SeedAsync()
        {
            var repo = _unitOfWork.Repository<AsmaulHusna>();

            // تحقق داخلي: إذا كانت 99 اسمًا موجودة → لا تفعل شيء
            var existingCount = await repo.CountAsync();
            if (existingCount >= 99)
            {
                _logger.LogInformation("Asmaul Husna already fully seeded ({Count}/99 names), skipping.", existingCount);
                return;
            }

            _logger.LogInformation("Starting Asmaul Husna seeding... Current: {Count}/99", existingCount);

            var json = await File.ReadAllTextAsync("C:\\Users\\moner\\source\\repos\\sukun\\apps\\backEnd\\Sukun\\Sukun.Application\\SeedData\\Names_Of_Allah.json");
            var data = JsonSerializer.Deserialize<List<SeedAllahName>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            foreach (var item in data)
            {
                var asmaulHusna = new AsmaulHusna
                {
                    Id = Guid.NewGuid(),
                    NameArabic = item.Name,
                    MeaningArabic = item.Text,
                    Number =item.Id,
                    CreateAt = DateTime.Now,

                };
               await repo.AddAsync(asmaulHusna);
            }
            await _unitOfWork.CompleteAsync();
        }
    }
}
