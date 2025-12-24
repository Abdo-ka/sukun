using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Text.Json;

namespace Sukun.Application.Seeder.Remembrance_entity
{
    public class RemembranceSeederService : IRemembranceSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RemembranceSeederService> _logger;

        public RemembranceSeederService(
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

                var existingCount = await remembranceRepo.CountAsync();
                if (existingCount > 50) // عدد تقريبي للأذكار اليومية
                {
                    _logger.LogInformation("Remembrances already seeded ({Count}), skipping.", existingCount);
                    return;
                }

                _logger.LogInformation("Starting Remembrances seeding from external sources...");

                // الفئات الـ 5 الثابتة
                var categories = await EnsureFiveCategoriesAsync(categoryRepo);

                // مصدر 1: أذكار الصباح والمساء (Seen-Arabic)
                await SeedFromMorningEveningAsync("https://raw.githubusercontent.com/Seen-Arabic/Morning-And-Evening-Adhkar-DB/main/ar.json", categories);

                // مصدر 2: حصن المسلم (rn0x)
                await SeedFromHisnAlMuslimAsync("https://raw.githubusercontent.com/rn0x/Adhkar-json/main/adhkar.json", categories);

                _logger.LogInformation("Remembrances seeding completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Remembrances seeding");
                throw;
            }
        }

        private async Task<Dictionary<string, Guid>> EnsureFiveCategoriesAsync(IRepository<RemembranceCategory> categoryRepo)
        {
            var categoryNames = new[]
            {
                "أذكار الصباح",
                "أذكار المساء",
                "أذكار قبل النوم",
                "أذكار الاستيقاظ",
                "أذكار الصلاة"
            };

            var categories = new Dictionary<string, Guid>();

            foreach (var name in categoryNames)
            {
                var existing = await categoryRepo.FirstOrDefaultAsync(c => c.NameAr == name);
                if (existing == null)
                {
                    existing = new RemembranceCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        NameAr = name,
                        CreateAt = DateTime.UtcNow
                    };
                    await categoryRepo.AddAsync(existing);
                }
                categories[name] = existing.Id;
            }

            await _unitOfWork.CompleteAsync();
            return categories;
        }

        private async Task SeedFromMorningEveningAsync(string url, Dictionary<string, Guid> categories)
        {
            var client = _httpClientFactory.CreateClient();
            var json = await client.GetStringAsync(url);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // إذا كان الـ root Array مباشرة
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in root.EnumerateArray())
                {
                    string text = "";
                    int count = 1;

                    if (item.TryGetProperty("text", out var textProp))
                        text = textProp.GetString()?.Trim() ?? "";
                    else if (item.TryGetProperty("content", out textProp))
                        text = textProp.GetString()?.Trim() ?? "";

                    if (string.IsNullOrEmpty(text)) continue;

                    if (item.TryGetProperty("count", out var countProp))
                        count = countProp.GetInt32();

                    // تحديد الفئة بناءً على الكلمات المفتاحية في النص أو عنوان
                    Guid categoryId = categories["أذكار الصباح"]; // افتراضي

                    var lowerText = text.ToLower();
                    if (lowerText.Contains("مساء") || lowerText.Contains("ليل") || lowerText.Contains("نوم"))
                        categoryId = categories["أذكار المساء"];
                    else if (lowerText.Contains("استيقاظ") || lowerText.Contains("يقظة"))
                        categoryId = categories["أذكار الاستيقاظ"];
                    else if (lowerText.Contains("صلاة") || lowerText.Contains("تسبيح"))
                        categoryId = categories["أذكار الصلاة"];
                    else if (lowerText.Contains("نوم"))
                        categoryId = categories["أذكار قبل النوم"];

                    await AddRemembranceAsync(text, count, categoryId);
                }
            }
            // إذا كان Object مع morning/evening
            else if (root.ValueKind == JsonValueKind.Object)
            {
                if (root.TryGetProperty("morning", out var morningArray))
                    await SeedRemembrancesFromArrayAsync(morningArray, categories["أذكار الصباح"]);

                if (root.TryGetProperty("evening", out var eveningArray))
                    await SeedRemembrancesFromArrayAsync(eveningArray, categories["أذكار المساء"]);
            }
        }

        private async Task AddRemembranceAsync(string arabicText, int count, Guid categoryId)
        {
            var remembranceRepo = _unitOfWork.Repository<Remembrance>();
            var contentRepo = _unitOfWork.Repository<RemembranceContent>();

            // تجنب التكرار
            var exists = await contentRepo.ExistsAsync(c => c.CustomContent == arabicText);
            if (exists) return;

            var remembrance = new Remembrance
            {
                Id = Guid.NewGuid(),
                Title = "ذكر من السنة",
                RecommendedCount = count,
                IsDaily = true,
                CreateAt = DateTime.UtcNow
            };

            remembrance.RemembranceCategoryLinks.Add(new RemembranceCategoryLinks { RemembranceCategoryId = categoryId });

            var content = new RemembranceContent
            {
                Id = Guid.NewGuid(),
                RemembranceId = remembrance.Id,
                SourceType = SourceType.Custom,
                CustomContent = arabicText,
                CreateAt = DateTime.UtcNow
            };

            remembrance.Contents.Add(content);

            await remembranceRepo.AddAsync(remembrance);
        }
        private async Task SeedFromHisnAlMuslimAsync(string url, Dictionary<string, Guid> categories)
        {
            var client = _httpClientFactory.CreateClient();
            var json = await client.GetStringAsync(url);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.ValueKind != JsonValueKind.Array) return;

            foreach (var catElement in root.EnumerateArray())
            {
                if (!catElement.TryGetProperty("category", out var catProp)) continue;

                var categoryName = catProp.GetString()?.Trim() ?? "";

                Guid? categoryId = null;
                if (categoryName.Contains("نوم") || categoryName.Contains("قبل النوم"))
                    categoryId = categories["أذكار قبل النوم"];
                else if (categoryName.Contains("استيقاظ") || categoryName.Contains("يقظة"))
                    categoryId = categories["أذكار الاستيقاظ"];
                else if (categoryName.Contains("صلاة") || categoryName.Contains("تسبيح"))
                    categoryId = categories["أذكار الصلاة"];

                if (categoryId == null || !catElement.TryGetProperty("array", out var itemsArray)) continue;

                await SeedRemembrancesFromArrayAsync(itemsArray, categoryId.Value);
            }
        }

        private async Task SeedRemembrancesFromArrayAsync(JsonElement itemsArray, Guid categoryId)
        {
            var remembranceRepo = _unitOfWork.Repository<Remembrance>();
            var contentRepo = _unitOfWork.Repository<RemembranceContent>();

            int order = 1;
            foreach (var item in itemsArray.EnumerateArray())
            {
                string text = "";
                int count = 1;

                if (item.TryGetProperty("text", out var textProp))
                    text = textProp.GetString()?.Trim() ?? "";
                else if (item.TryGetProperty("Text", out textProp))
                    text = textProp.GetString()?.Trim() ?? "";

                if (string.IsNullOrEmpty(text)) continue;

                if (item.TryGetProperty("count", out var countProp))
                    count = countProp.GetInt32();
                else if (item.TryGetProperty("Count", out countProp))
                    count = countProp.GetInt32();

                // تجنب التكرار
                var exists = await contentRepo.ExistsAsync(c => c.CustomContent == text);
                if (exists) continue;

                var remembrance = new Remembrance
                {
                    Id = Guid.NewGuid(),
                    Title = "ذكر من السنة",
                    RecommendedCount = count,
                    Benefits = null,
                    IsDaily = true,
                    CreateAt = DateTime.UtcNow
                };

                remembrance.RemembranceCategoryLinks.Add(new RemembranceCategoryLinks { RemembranceCategoryId = categoryId });

                var content = new RemembranceContent
                {
                    Id = Guid.NewGuid(),
                    RemembranceId = remembrance.Id,
                    SourceType = SourceType.Custom,
                    CustomContent = text,
                    CreateAt = DateTime.UtcNow
                };

                remembrance.Contents.Add(content);

                await remembranceRepo.AddAsync(remembrance);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
    }
   

