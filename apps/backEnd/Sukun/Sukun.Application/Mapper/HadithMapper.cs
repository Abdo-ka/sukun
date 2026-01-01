using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class HadithMapper
    {
        public static HadithCategoryResponseDto ToResponseDto(this HadithCategory category)
        {
            return new HadithCategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                HadithsCount = category.Hadiths.Count(h => !h.IsDeleted)
            };
        }

        public static HadithListResponseDto ToListDto(this Hadith hadith)
        {
            return new HadithListResponseDto
            {
                Id = hadith.Id,
                Reference = hadith.Reference,
                Text = hadith.Text,
                Grade = hadith.Grade,
                BookName = hadith.Book is not null ? hadith.Book.Name : "",
                SectionName = hadith.Section is not null ? hadith.Section.Name : "",
                HadithNumber = hadith.HadithNumber,
                CategoryId = hadith.CategoryId,
                CategoryName = hadith.Category?.Name ?? string.Empty
            };
        }

        public static HadithResponseDto ToResponseDto(this Hadith hadith)
        {
            return new HadithResponseDto
            {
                Id = hadith.Id,
                Reference = hadith.Reference,
                Text = hadith.Text,
                Grade = hadith.Grade,
                BookId = hadith.Book?.Id ?? null,
                BookName = hadith.Book?.Name ?? string.Empty,
                SectionId = hadith.Section?.Id ?? null,
                SectionName = hadith.Section?.Name ?? string.Empty,
                HadithNumber = hadith.HadithNumber,
                CategoryId = hadith.CategoryId,
                CategoryName = hadith.Category?.Name ?? string.Empty,
                Explanations = hadith.Explanations
                    .OrderBy(e => e.CreateAt)
                    .Select(e => e.ToResponseDto())
                    .ToList()
            };
        }

        public static HadithExplanationResponseDto ToResponseDto(this HadithExplanation explanation)
        {
            return new HadithExplanationResponseDto
            {
                Id = explanation.Id,
                Scholar = explanation.Scholar,
                Explanation = explanation.Explanation
            };
        }

        // للـ Create (من DTO إلى Entity)
        public static Hadith ToEntity(this HadithCreateDto dto, Guid? id = null)
        {
            var hadith = new Hadith
            {
                Id = id ?? Guid.NewGuid(),
                CategoryId = dto.CategoryId,
                Reference = dto.Reference,
                Text = dto.Text,
                Grade = dto.Grade,
                HadithNumber = dto.HadithNumber,
                CreateAt = DateTime.UtcNow,
                BookId = dto.BookId,
                SectionId = dto.SectionId
            };

            if (dto.Explanations != null && dto.Explanations.Any())
            {
                hadith.Explanations = dto.Explanations.Select(e => new HadithExplanation
                {
                    Id = Guid.NewGuid(),
                    Scholar = e.Scholar,
                    Explanation = e.Explanation,
                    HadithId = hadith.Id,
                    CreateAt = DateTime.UtcNow
                }).ToList();
            }

            return hadith;
        }
    }
}


