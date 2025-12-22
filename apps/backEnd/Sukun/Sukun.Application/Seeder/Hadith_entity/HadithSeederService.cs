using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Text.Json;

namespace Sukun.Application.Seeder.Hadith_entity
{
    public class HadithSeederService : IHadithSeederService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IHttpClientFactory _httpClientFactory;
            private readonly ILogger<HadithSeederService> _logger;

            public HadithSeederService(
                IUnitOfWork unitOfWork,
                IHttpClientFactory httpClientFactory,
                ILogger<HadithSeederService> logger)
            {
                _unitOfWork = unitOfWork;
                _httpClientFactory = httpClientFactory;
                _logger = logger;
            }

            public async Task SeedHadithsAsync()
            {
                try
                {
                    var hadithRepo = _unitOfWork.Repository<Hadith>();
                    var categoryRepo = _unitOfWork.Repository<HadithCategory>();

                    // تحقق إذا كانت الأحاديث موجودة بالفعل
                    var existingCount = await hadithRepo.CountAsync();
                    if (existingCount >= 42) // الأربعين النووية + 2 إضافي
                    {
                        _logger.LogInformation("Hadiths already seeded ({Count}), skipping.", existingCount);
                        return;
                    }

                    _logger.LogInformation("Starting Hadith seeding from external API...");

                    var client = _httpClientFactory.CreateClient();

                    // مصدر موثوق: الأربعين النووية من مشروع fawazahmed0 (مشهور وموثوق)
                    var url = "https://cdn.jsdelivr.net/gh/fawazahmed0/hadith-api@1/editions/ara-nawawi.json";

                    var json = await client.GetStringAsync(url);

                    var apiResponse = JsonSerializer.Deserialize<HadithApiResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (apiResponse?.Hadiths == null || !apiResponse.Hadiths.Any())
                    {
                        _logger.LogError("Failed to fetch hadiths from external API");
                        return;
                    }

                    // فئة افتراضية إذا لم توجد
                    var defaultCategory = await categoryRepo.FirstOrDefaultAsync(c => c.Name == "الأخلاق والآداب");
                    if (defaultCategory == null)
                    {
                        defaultCategory = new HadithCategory
                        {
                            Id = Guid.NewGuid(),
                            Name = "الأخلاق والآداب",
                            Description = "أحاديث في الأخلاق والآداب من الأربعين النووية",
                            CreateAt = DateTime.UtcNow
                        };
                        await categoryRepo.AddAsync(defaultCategory);
                        await _unitOfWork.CompleteAsync();
                    }

                    int addedCount = 0;
                    foreach (var apiHadith in apiResponse.Hadiths)
                    {
                        // تجنب التكرار بناءً على HadithNumber
                        var existing = await hadithRepo.FirstOrDefaultAsync(h => h.HadithNumber == apiHadith.HadithNumber && h.BookName.Contains("نووي"));
                        if (existing != null) continue;

                        var hadith = new Hadith
                        {
                            Id = Guid.NewGuid(),
                            CategoryId = defaultCategory.Id,
                            Reference = $"الأربعين النووية {apiHadith.HadithNumber}",
                            Text = apiHadith.Text?.Trim() ?? "غير متوفر",
                            Grade = HadithGrade.Sahih, // كلها صحيحة أو حسنة
                            GradedBy = "الإمام النووي",
                            GradeExplanation = apiHadith.Grade ?? "متفق عليه أو حسن",
                            BookName = "الأربعين النووية",
                            ChapterName = null,
                            HadithNumber = apiHadith.HadithNumber,
                            CreateAt = DateTime.UtcNow
                        };

                        // إضافة شرح بسيط إذا وجد
                        if (!string.IsNullOrWhiteSpace(apiHadith.Explanation))
                        {
                            hadith.Explanations.Add(new HadithExplanation
                            {
                                Id = Guid.NewGuid(),
                                HadithId = hadith.Id,
                                Scholar = "الإمام النووي",
                                Explanation = apiHadith.Explanation.Trim(),
                                CreateAt = DateTime.UtcNow
                            });
                        }

                        await hadithRepo.AddAsync(hadith);
                        addedCount++;
                    }

                    if (addedCount > 0)
                    {
                        await _unitOfWork.CompleteAsync();
                        _logger.LogInformation("Hadith seeding completed. Added {AddedCount} hadiths from Arba'een Nawawiyyah.", addedCount);
                    }
                    else
                    {
                        _logger.LogInformation("No new hadiths to add.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during Hadith seeding from external source");
                    throw;
                }
            }
        }
    
}

