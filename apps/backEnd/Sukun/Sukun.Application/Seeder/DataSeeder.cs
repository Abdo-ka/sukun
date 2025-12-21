using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sukun.Application.Seeder
{

    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // تجنب الـ Seed مرتين
            if (await context.Tags.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("C:\\Users\\moner\\source\\repos\\sukun\\apps\\backEnd\\Sukun\\Sukun.Application\\SeedData\\narratives-seed.json");
            var data = JsonSerializer.Deserialize<SeedData>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Seed Tags أولاً
            var tags = data.Tags.Select(t => new Tag
            {
                Id = Guid.NewGuid(),
                TagType = t.TagType,
                NameAr = t.NameAr,
                NameEn = t.NameEn,
                IconUrl = t.IconUrl
            }).ToList();

            context.Tags.AddRange(tags);
            await context.SaveChangesAsync();

            // إنشاء خريطة TagType → Tag Id
            var tagMap = tags.ToDictionary(t => t.TagType, t => t.Id);

            // Seed Narratives
            var narratives = new List<Narrative>();
            foreach (var item in data.Narratives)
            {
                var narrative = new Narrative
                {
                    Id = Guid.NewGuid(),
                    Title = item.Title,
                    TitleAr = item.TitleAr ?? item.Title,
                    Type = item.Type,
                    ShortDescription = item.ShortDescription,
                    CoverImageUrl = item.CoverImageUrl,
                    IsFeatured = item.IsFeatured,
                    ParentId = item.ParentId,
                    CreateAt = DateTime.UtcNow,
                    Tags = item.TagTypes.Select(tt => tags.First(t => t.TagType == tt)).ToList()
                };

                // إضافة Sections إذا وجدت
                if (item.Sections != null)
                {
                    narrative.Sections = item.Sections.Select((s, index) => new NarrativeSection
                    {
                        Id = Guid.NewGuid(),
                        NarrativeId = narrative.Id,
                        Title = s.Title,
                        Content = s.Content,
                        DisplayOrder = s.DisplayOrder ?? index + 1,
                        MediaUrl = s.MediaUrl,
                        CreateAt = DateTime.UtcNow
                    }).ToList();
                }

                narratives.Add(narrative);
            }

            // ربط الأبناء بالآباء
            foreach (var item in data.Narratives.Where(n => n.Children != null))
            {
                var parent = narratives.First(n => n.Title == item.Title);
                foreach (var childItem in item.Children)
                {
                    var child = new Narrative
                    {
                        Id = Guid.NewGuid(),
                        Title = childItem.Title,
                        TitleAr = childItem.TitleAr ?? childItem.Title,
                        Type = childItem.Type,
                        ShortDescription = $"سيرة {childItem.Title}",
                        CoverImageUrl = "https://example.com/covers/default.jpg",
                        ParentId = parent.Id,
                        CreateAt = DateTime.UtcNow,
                        Tags = childItem.TagTypes.Select(tt => tags.First(t => t.TagType == tt)).ToList()
                    };
                    narratives.Add(child);
                }
            }

            context.Narratives.AddRange(narratives);
            await context.SaveChangesAsync();
        }

        // Classes للـ Deserialize
        private class SeedData
        {
            public List<SeedTag> Tags { get; set; } = new();
            public List<SeedNarrative> Narratives { get; set; } = new();
        }

        private class SeedTag
        {
            public NarrativeTag TagType { get; set; }
            public string NameAr { get; set; } = string.Empty;
            public string NameEn { get; set; } = string.Empty;
            public string? IconUrl { get; set; }
        }

        private class SeedNarrative
        {
            public string Title { get; set; } = string.Empty;
            public string? TitleAr { get; set; }
            public ContentType Type { get; set; }
            public string? ShortDescription { get; set; }
            public string? CoverImageUrl { get; set; }
            public bool IsFeatured { get; set; }
            public Guid? ParentId { get; set; }
            public List<NarrativeTag> TagTypes { get; set; } = new();
            public List<SeedSection>? Sections { get; set; }
            public List<SeedChild>? Children { get; set; }
        }

        private class SeedSection
        {
            public string Title { get; set; } = string.Empty;
            public string Content { get; set; } = string.Empty;
            public int? DisplayOrder { get; set; }
            public string? MediaUrl { get; set; }
        }

        private class SeedChild
        {
            public string Title { get; set; } = string.Empty;
            public string? TitleAr { get; set; }
            public ContentType Type { get; set; }
            public List<NarrativeTag> TagTypes { get; set; } = new();
        }
    }
}
