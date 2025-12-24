using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Text.Json;

namespace Sukun.Application.Seeder.Remembrance_entity
{
    public class RemembranceSeederServicev2 : IRemembranceSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RemembranceSeederService> _logger;

        public RemembranceSeederServicev2(
            IUnitOfWork unitOfWork,
            IHttpClientFactory httpClientFactory,
            ILogger<RemembranceSeederService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SeedRemembrancesAsync()
        {
            try
            {
                var categoryRepo = _unitOfWork.Repository<RemembranceCategory>();
                var remembranceRepo = _unitOfWork.Repository<Remembrance>();
                var contentRepo = _unitOfWork.Repository<RemembranceContent>();
                var categories = new Dictionary<string, Guid>
                {
                    { "أذكار الصباح", Guid.NewGuid() },
                    { "أذكار المساء", Guid.NewGuid() },
                    { "أذكار قبل النوم", Guid.NewGuid() },
                    { "أذكار الاستيقاظ", Guid.NewGuid() },
                    { "أذكار الصلاة", Guid.NewGuid() }
                };
                // تحقق من وجود الفئات
                //    var existingCategories = await categoryRepo.CountAsync();
                //    if (existingCategories >= 5)
                //    {
                //        _logger.LogInformation("Remembrance categories already seeded, skipping.");
                //        return;
                //    }

                //    // إنشاء الفئات الـ 5 الثابتة


                //    foreach (var kv in categories)
                //    {
                //        var category = new RemembranceCategory
                //        {
                //            Id = kv.Value,
                //            Name = kv.Key,
                //            NameAr = kv.Key,
                //            CreateAt = DateTime.UtcNow
                //        };

                //        await categoryRepo.AddAsync(category);
                //    }

                //    await _unitOfWork.CompleteAsync();

                //    _logger.LogInformation("Seeded 5 remembrance categories.");

                // جلب أذكار الصباح والمساء من مصدر خارجي (Seen-Arabic repo)
                await SeedFromExternalSourceAsync("https://raw.githubusercontent.com/Seen-Arabic/Morning-And-Evening-Adhkar-DB/main/ar.json", categories["أذكار الصباح"], categories["أذكار المساء"]);

                // جلب أذكار النوم والاستيقاظ من rn0x repo (حصن المسلم)
                await SeedFromHisnAlMuslimAsync("https://raw.githubusercontent.com/rn0x/Adhkar-json/main/adhkar.json", categories);

                // أذكار الصلاة (من حصن المسلم أو مصدر آخر)
                await SeedPrayerRemembrancesAsync(categories["أذكار الصلاة"]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Remembrances seeding");
                throw;
            }
        }

        private async Task SeedFromExternalSourceAsync(string url, Guid morningId, Guid eveningId)
        {
            var client = _httpClientFactory.CreateClient();
            var json = await client.GetStringAsync(url);

            // افتراض هيكل JSON من Seen-Arabic (morning و evening arrays)
            var apiData = JsonSerializer.Deserialize<ApiMorningEveningData>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (apiData?.Morning != null)
                await SeedRemembrancesFromListAsync(apiData.Morning, morningId);

            if (apiData?.Evening != null)
                await SeedRemembrancesFromListAsync(apiData.Evening, eveningId);
        }

        private async Task SeedFromHisnAlMuslimAsync(string url, Dictionary<string, Guid> categories)
        {
            var client = _httpClientFactory.CreateClient();
            var json = await client.GetStringAsync(url);

            var apiCategories = JsonSerializer.Deserialize<List<ApiHisnCategory>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            foreach (var apiCat in apiCategories)
            {
                var categoryName = apiCat.Category.Trim();

                Guid? categoryId = null;
                if (categoryName.Contains("نوم") || categoryName.Contains("قبل النوم"))
                    categoryId = categories["أذكار قبل النوم"];
                else if (categoryName.Contains("استيقاظ"))
                    categoryId = categories["أذكار الاستيقاظ"];

                if (categoryId == null || apiCat.Array == null) continue;

                foreach (var apiItem in apiCat.Array)
                {
                    var remembrance = new Remembrance
                    {
                        Id = Guid.NewGuid(),
                        Title = $"ذكر من {categoryName}",
                        RecommendedCount = apiItem.Count,
                        CreateAt = DateTime.UtcNow
                    };

                    remembrance.RemembranceCategoryLinks.Add(new RemembranceCategoryLinks { RemembranceCategoryId = categoryId.Value });

                    var content = new RemembranceContent
                    {
                        Id = Guid.NewGuid(),
                        RemembranceId = remembrance.Id,
                        SourceType = SourceType.Custom,
                        CustomContent = apiItem.Text.Trim(),
                        CreateAt = DateTime.UtcNow
                    };

                    remembrance.Contents.Add(content);

                    await _unitOfWork.Remembrances.AddAsync(remembrance);
                }
            }

            await _unitOfWork.CompleteAsync();
        }

        private async Task SeedPrayerRemembrancesAsync(Guid categoryId)
        {
            // أذكار الصلاة الشهيرة من السنة (من مصادر موثوقة مثل حصن المسلم)
            var prayerDuas = new List<(string title, string text, int count)>
            {
                ("التسبيح بعد الصلاة", "سبحان الله 33، الحمد لله 33، الله أكبر 34", 1),
                ("الاستغفار بعد الصلاة", "أستغفر الله (3 مرات)", 3),
                ("دعاء بعد السلام", "اللهم أنت السلام ومنك السلام تباركت يا ذا الجلال والإكرام", 1)
                // أضف المزيد من مصادر موثوقة
            };

            foreach (var dua in prayerDuas)
            {
                var remembrance = new Remembrance
                {
                    Id = Guid.NewGuid(),
                    Title = dua.title,
                    RecommendedCount = dua.count,
                    CreateAt = DateTime.UtcNow
                };

                remembrance.RemembranceCategoryLinks.Add(new RemembranceCategoryLinks { RemembranceCategoryId = categoryId });

                var content = new RemembranceContent
                {
                    Id = Guid.NewGuid(),
                    RemembranceId = remembrance.Id,
                    SourceType = SourceType.Custom,
                    CustomContent = dua.text,
                    CreateAt = DateTime.UtcNow
                };

                remembrance.Contents.Add(content);

                await _unitOfWork.Remembrances.AddAsync(remembrance);
            }

            await _unitOfWork.CompleteAsync();
        }

        private async Task SeedRemembrancesFromListAsync(List<ApiDailyAdhkarItem> items, Guid categoryId)
        {
            foreach (var item in items)
            {
                var remembrance = new Remembrance
                {
                    Id = Guid.NewGuid(),
                    Title = item.Title ?? "ذكر يومي",
                    RecommendedCount = item.Count ?? 1,
                    CreateAt = DateTime.UtcNow
                };

                remembrance.RemembranceCategoryLinks.Add(new RemembranceCategoryLinks { RemembranceCategoryId = categoryId });

                var content = new RemembranceContent
                {
                    Id = Guid.NewGuid(),
                    RemembranceId = remembrance.Id,
                    SourceType = SourceType.Custom,
                    CustomContent = item.Text,
                    CreateAt = DateTime.UtcNow
                };

                remembrance.Contents.Add(content);

                await _unitOfWork.Remembrances.AddAsync(remembrance);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
    }
   

