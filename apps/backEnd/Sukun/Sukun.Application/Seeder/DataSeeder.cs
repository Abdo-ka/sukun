using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Context;
using System.Text.Json;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "narratives-seed.json");
        var json = await File.ReadAllTextAsync("C:\\Users\\moner\\source\\repos\\sukun\\apps\\backEnd\\Sukun\\Sukun.Application\\SeedData\\narratives-seed.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<SeedData>(json, options)!;

        // 1. Seed Tags (مع فحص التكرار بالاسم)
        var tagMap = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

        foreach (var seedTag in data.Tags)
        {
            var normalizedName = seedTag.Name.Trim().ToLowerInvariant();
            var existingTag = await context.Tags
                .FirstOrDefaultAsync(t => t.Name.ToLower() == normalizedName);

            Guid tagId;
            if (existingTag != null)
            {
                tagId = existingTag.Id;
            }
            else
            {
                var newTag = new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = seedTag.Name.Trim(),
                    NameAr = seedTag.NameAr?.Trim() ?? seedTag.Name.Trim()
                };

                context.Tags.Add(newTag);
                await context.SaveChangesAsync(); // للحصول على Id
                tagId = newTag.Id;
            }

            tagMap[seedTag.Name.Trim()] = tagId;
            if (!string.IsNullOrEmpty(seedTag.NameAr))
                tagMap[seedTag.NameAr.Trim()] = tagId;
        }

        // 2. Seed Categories (مع فحص التكرار بالعنوان + Parent)
        var categoryMap = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

        foreach (var catItem in data.Categories)
        {
            var mainTitleNorm = catItem.Title.Trim().ToLowerInvariant();
            var existingMain = await context.Categories
                .FirstOrDefaultAsync(c => c.Title.ToLower() == mainTitleNorm && c.ParentId == null);

            Guid mainCategoryId;
            if (existingMain != null)
            {
                mainCategoryId = existingMain.Id;
            }
            else
            {
                var mainCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Title = catItem.Title.Trim(),
                    TitleAr = catItem.TitleAr?.Trim() ?? catItem.Title.Trim(),
                    IsMainSection = catItem.IsMainSection,
                    Order = catItem.Order,
                    ParentId = null
                };

                context.Categories.Add(mainCategory);
                await context.SaveChangesAsync();
                mainCategoryId = mainCategory.Id;
            }

            categoryMap[catItem.Title.Trim()] = mainCategoryId;

            // الأقسام الفرعية
            if (catItem.Children != null && catItem.Children.Any())
            {
                foreach (var childItem in catItem.Children)
                {
                    var childTitleNorm = childItem.Title.Trim().ToLowerInvariant();
                    var existingChild = await context.Categories
                        .FirstOrDefaultAsync(c =>
                            c.Title.ToLower() == childTitleNorm &&
                            c.ParentId == mainCategoryId);

                    Guid subCategoryId;
                    if (existingChild != null)
                    {
                        subCategoryId = existingChild.Id;
                    }
                    else
                    {
                        var subCategory = new Category
                        {
                            Id = Guid.NewGuid(),
                            Title = childItem.Title.Trim(),
                            TitleAr = childItem.TitleAr?.Trim() ?? childItem.Title.Trim(),
                            IsMainSection = false,
                            Order = childItem.Order,
                            ParentId = mainCategoryId
                        };

                        context.Categories.Add(subCategory);
                        await context.SaveChangesAsync();
                        subCategoryId = subCategory.Id;
                    }

                    categoryMap[childItem.Title.Trim()] = subCategoryId;
                }
            }
        }

        // 3. Seed Narratives + Sections (مع فحص التكرار بالعنوان)
        var narrativeMap = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

        foreach (var item in data.Narratives)
        {
            var titleNorm = item.Title.Trim().ToLowerInvariant();
            var existingNarrative = await context.Narratives
                .FirstOrDefaultAsync(n => n.Title.ToLower() == titleNorm);

            Guid narrativeId;
            if (existingNarrative != null)
            {
                narrativeId = existingNarrative.Id;
            }
            else
            {
                var narrative = new Narrative
                {
                    Id = Guid.NewGuid(),
                    Title = item.Title.Trim(),
                    TitleAr = item.TitleAr?.Trim() ?? item.Title.Trim(),
                    Type = item.Type,
                    ShortDescription = item.ShortDescription,
                    IsFeatured = item.IsFeatured,
                    CreateAt = DateTime.UtcNow
                };

                // Sections
                if (item.Sections != null && item.Sections.Any())
                {
                    narrative.Sections = item.Sections.Select((s, index) => new NarrativeSection
                    {
                        Id = Guid.NewGuid(),
                        Title = s.Title.Trim(),
                        Content = s.Content,
                        DisplayOrder = s.DisplayOrder ?? index + 1,
                        CreateAt = DateTime.UtcNow
                    }).ToList();
                }

                context.Narratives.Add(narrative);
                await context.SaveChangesAsync();
                narrativeId = narrative.Id;
            }

            narrativeMap[item.Title.Trim()] = narrativeId;
        }

        // 4. ربط Narrative مع Category (فقط إذا لم يكن مرتبطًا)
        foreach (var item in data.Narratives)
        {
            if (item.CategoryTitles == null || !item.CategoryTitles.Any()) continue;

            var narrativeId = narrativeMap[item.Title.Trim()];

            foreach (var catTitle in item.CategoryTitles)
            {
                if (categoryMap.TryGetValue(catTitle.Trim(), out var categoryId))
                {
                    var alreadyExists = await context.NarrativeCategories
                        .AnyAsync(nc => nc.NarrativeId == narrativeId && nc.CategoryId == categoryId);

                    if (!alreadyExists)
                    {
                        context.NarrativeCategories.Add(new NarrativeCategory
                        {
                            NarrativeId = narrativeId,
                            CategoryId = categoryId,
                            DisplayOrder = 0
                        });
                    }
                }
            }
        }

        await context.SaveChangesAsync();

        // 5. ربط Narrative مع Tags (فقط إذا لم يكن مرتبطًا)
        foreach (var item in data.Narratives)
        {
            if (item.TagNames == null || !item.TagNames.Any()) continue;

            var narrativeId = narrativeMap[item.Title.Trim()];

            foreach (var tagName in item.TagNames)
            {
                if (tagMap.TryGetValue(tagName.Trim(), out var tagId))
                {
                    var alreadyExists = await context.NarrativeTags
                        .AnyAsync(nt => nt.NarrativeId == narrativeId && nt.TagId == tagId);

                    if (!alreadyExists)
                    {
                        context.NarrativeTags.Add(new NarrativeTags
                        {
                            NarrativeId = narrativeId,
                            TagId = tagId
                        });
                    }
                }
            }
        }

        await context.SaveChangesAsync();
    }

    // الكلاسات للـ Deserialize (نفس السابقة)
    private class SeedData
    {
        public List<SeedCategory> Categories { get; set; } = new();
        public List<SeedTag> Tags { get; set; } = new();
        public List<SeedNarrative> Narratives { get; set; } = new();
    }

    private class SeedCategory
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public bool IsMainSection { get; set; } = true;
        public int Order { get; set; } = 0;
        public List<SeedCategory>? Children { get; set; }
    }

    private class SeedTag
    {
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
    }

    private class SeedNarrative
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public bool IsFeatured { get; set; }
        public List<string>? CategoryTitles { get; set; }
        public List<string>? TagNames { get; set; }
        public List<SeedSection>? Sections { get; set; }
    }

    private class SeedSection
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int? DisplayOrder { get; set; }
    }
}