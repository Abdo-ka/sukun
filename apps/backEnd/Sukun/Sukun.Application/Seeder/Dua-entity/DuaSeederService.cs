using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sukun.Application.Seeder.Dua_entity
{

    public class DuaSeederService : IDuaSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DuaSeederService> _logger;

        public DuaSeederService(
            IUnitOfWork unitOfWork,
            IHttpClientFactory httpClientFactory,
            ILogger<DuaSeederService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task SeedDuasAsync()
        {
            try
            {
                var categoryRepo = _unitOfWork.Repository<DuaCategory>();
                var duaRepo = _unitOfWork.Repository<DuaItem>();

                // تحقق إذا كانت الأدعية موجودة (مثلاً أكثر من 100 دعاء)
                var existingCount = await duaRepo.CountAsync();
                if (existingCount > 100) // حصن المسلم يحتوي على حوالي 132-270 دعاء حسب النسخة
                {
                    _logger.LogInformation("Duas already seeded ({Count}), skipping.", existingCount);
                    return;
                }

                _logger.LogInformation("Starting Duas seeding from Hisn al-Muslim external source...");

                var client = _httpClientFactory.CreateClient();
                var url = "https://raw.githubusercontent.com/rn0x/Adhkar-json/main/adhkar.json";

                var json = await client.GetStringAsync(url);

                var apiDuas = JsonSerializer.Deserialize<List<ApiDuaCategory>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiDuas == null || !apiDuas.Any())
                {
                    _logger.LogError("Failed to fetch duas from external source");
                    return;
                }

                int addedCategories = 0;
                int addedDuas = 0;

                foreach (var apiCategory in apiDuas)
                {
                    // تجنب تكرار الفئة
                    var existingCategory = await categoryRepo.FirstOrDefaultAsync(c => c.NameAr == apiCategory.Category.Trim());
                    DuaCategory category;

                    if (existingCategory == null)
                    {
                        category = new DuaCategory
                        {
                            Id = Guid.NewGuid(),
                            Name = apiCategory.Category.Trim(),
                            NameAr = apiCategory.Category.Trim(),
                            CreateAt = DateTime.UtcNow
                        };

                        await categoryRepo.AddAsync(category);
                        addedCategories++;
                    }
                    else
                    {
                        category = existingCategory;
                    }

                    if (apiCategory.Array != null)
                    {
                        foreach (var apiItem in apiCategory.Array)
                        {
                            // تجنب التكرار بناءً على النص
                            var existingDua = await duaRepo.FirstOrDefaultAsync(d => d.ArabicText == apiItem.Text.Trim());
                            if (existingDua != null) continue;

                            var dua = new DuaItem
                            {
                                Id = Guid.NewGuid(),
                                CategoryId = category.Id,
                                Title = $"دعاء رقم {apiItem.Id}", // يمكن تحسين العنوان لاحقًا
                                ArabicText = apiItem.Text.Trim(),
                                Transliteration = null, // المصدر لا يحتوي على نطق، يمكن إضافته يدويًا لاحقًا
                                Translation = null, // لا يوجد ترجمة في هذا المصدر
                                Reference = "حصن المسلم",
                                RepeatCount = apiItem.Count,
                                Virtue = null,
                                DisplayOrder = apiItem.Id,
                                CreateAt = DateTime.UtcNow
                            };

                            await duaRepo.AddAsync(dua);
                            addedDuas++;
                        }
                    }
                }

                if (addedCategories > 0 || addedDuas > 0)
                {
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation("Duas seeding completed. Added {Categories} categories and {Duas} duas from Hisn al-Muslim.", addedCategories, addedDuas);
                }
                else
                {
                    _logger.LogInformation("No new duas or categories to add.");
                }

                // إضافة أدعية مشهورة أخرى يدويًا (من السنة، خارج حصن المسلم)
                await SeedAdditionalFamousDuasAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Duas seeding from external source");
                throw;
            }
        }

        private async Task SeedAdditionalFamousDuasAsync()
        {
            var categoryRepo = _unitOfWork.Repository<DuaCategory>();
            var duaRepo = _unitOfWork.Repository<DuaItem>();

            var famousCategory = await categoryRepo.FirstOrDefaultAsync(c => c.NameAr == "أدعية مشهورة من السنة");
            if (famousCategory == null)
            {
                famousCategory = new DuaCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "أدعية مشهورة من السنة",
                    NameAr = "أدعية مشهورة من السنة",
                    CreateAt = DateTime.UtcNow
                };
                await categoryRepo.AddAsync(famousCategory);
                await _unitOfWork.CompleteAsync();
            }

            var famousDuas = new List<DuaItem>
            {
                new DuaItem
                {
                    Id = Guid.NewGuid(),
                    CategoryId = famousCategory.Id,
                    Title = "دعاء الاستخارة",
                    ArabicText = "اللَّهُمَّ إِنِّي أَسْتَخِيرُكَ بِعِلْمِكَ وَأَسْتَقْدِرُكَ بِقُدْرَتِكَ وَأَسْأَلُكَ مِنْ فَضْلِكَ الْعَظِيمِ فَإِنَّكَ تَقْدِرُ وَلاَ أَقْدِرُ وَتَعْلَمُ وَلاَ أَعْلَمُ وَأَنْتَ عَلاَّمُ الْغُيُوبِ اللَّهُمَّ إِنْ كُنْتَ تَعْلَمُ أَنَّ هَذَا الأَمْرَ خَيْرٌ لِي فِي دِينِي وَمَعَاشِي وَعَاقِبَةِ أَمْرِي فَاقْدُرْهُ لِي وَيَسِّرْهُ لِي ثُمَّ بَارِكْ لِي فِيهِ وَإِنْ كُنْتَ تَعْلَمُ أَنَّ هَذَا الأَمْرَ شَرٌّ لِي فِي دِينِي وَمَعَاشِي وَعَاقِبَةِ أَمْرِي فَاصْرِفْهُ عَنِّي وَاصْرِفْنِي عَنْهُ وَاقْدُرْ لِي الْخَيْرَ حَيْثُ كَانَ ثُمَّ أَرْضِنِي",
                    Reference = "رواه البخاري",
                    RepeatCount = 1,
                    DisplayOrder = 1,
                    CreateAt = DateTime.UtcNow
                },
                new DuaItem
                {
                    Id = Guid.NewGuid(),
                    CategoryId = famousCategory.Id,
                    Title = "دعاء سيد الاستغفار",
                    ArabicText = "اللَّهُمَّ أَنْتَ رَبِّي لاَ إِلَهَ إِلاَّ أَنْتَ خَلَقْتَنِي وَأَنَا عَبْدُكَ وَأَنَا عَلَى عَهْدِكَ وَوَعْدِكَ مَا اسْتَطَعْتُ أَعُوذُ بِكَ مِنْ شَرِّ مَا صَنَعْتُ أَبُوءُ لَكَ بِنِعْمَتِكَ عَلَيَّ وَأَبُوءُ بِذَنْبِي فَاغْفِرْ لِي فَإِنَّهُ لاَ يَغْفِرُ الذُّنُوبَ إِلاَّ أَنْتَ",
                    Reference = "رواه البخاري",
                    RepeatCount = 1,
                    DisplayOrder = 2,
                    CreateAt = DateTime.UtcNow
                },
                // أضف المزيد من الأدعية المشهورة هنا...
            };

            foreach (var dua in famousDuas)
            {
                var exists = await duaRepo.ExistsAsync(d => d.ArabicText == dua.ArabicText);
                if (!exists)
                {
                    await duaRepo.AddAsync(dua);
                }
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
