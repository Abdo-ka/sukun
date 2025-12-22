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
                BookName = hadith.BookName,
                ChapterName = hadith.ChapterName,
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
                GradedBy = hadith.GradedBy,
                GradeExplanation = hadith.GradeExplanation,
                BookName = hadith.BookName,
                ChapterName = hadith.ChapterName,
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
                GradedBy = dto.GradedBy,
                GradeExplanation = dto.GradeExplanation,
                BookName = dto.BookName,
                ChapterName = dto.ChapterName,
                HadithNumber = dto.HadithNumber,
                CreateAt = DateTime.UtcNow
            };

            if (dto.Explanations != null)
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


