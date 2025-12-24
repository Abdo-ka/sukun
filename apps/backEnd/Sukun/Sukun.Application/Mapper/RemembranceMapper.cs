using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Application.Dtos.RemembranceCategory.Request;
using Sukun.Application.Dtos.RemembranceCategory.Response;
using Sukun.Application.Dtos.RemembranceContent.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class RemembranceMapper
    {
        public static RemembranceCategoryResponseDto ToResponseDto(this RemembranceCategory category)
        {
            return new RemembranceCategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                NameAr = category.NameAr,
                NameEn = category.NameEn,
                IsDaily = category.IsDaily,
                RemembrancesCount = category.RemembranceCategoryLinks.Count(r => !r.IsDeleted)
            };
        }

        public static RemembranceCategoryWithRemembrancesDto ToCategoryWithRemembrancesDto(this RemembranceCategory category)
        {
            return new RemembranceCategoryWithRemembrancesDto
            {
                Id = category.Id,
                Name = category.Name,
                NameAr = category.NameAr,
                NameEn = category.NameEn,
                IsDaily = category.IsDaily,
                RemembrancesCount = category.RemembranceCategoryLinks.Count(r => !r.IsDeleted),
                Remembrances = category.RemembranceCategoryLinks
                    .Where(r => !r.IsDeleted)
                    .Select(r => r.Remembrance.ToResponseDto())
                    .ToList()
            };
        }

        public static RemembranceResponseDto ToResponseDto(this Remembrance remembrance)
        {
            return new RemembranceResponseDto
            {
                Id = remembrance.Id,
                Title = remembrance.Title,
                Text = remembrance.Text,
                RecommendedCount = remembrance.RecommendedCount,
                Benefits = remembrance.Benefits,
                IsDaily = remembrance.IsDaily,
                CategoryNames = remembrance.RemembranceCategoryLinks
                    .Where(c => !c.IsDeleted)
                    .Select(c => c.RemembranceCategory.NameAr)
                    .ToList(),
                Contents = remembrance.Contents
                    .Where(c => !c.IsDeleted)
                    .Select(c => c.ToContentDto())
                    .ToList()
            };
        }

        public static RemembranceContentResponseDto ToContentDto(this RemembranceContent content)
        {
            return new RemembranceContentResponseDto
            {
                SourceType = content.SourceType,
                SourceId = content.SourceId,
                CustomContent = content.CustomContent,
                DisplayText = content.CustomContent ?? string.Empty // سيتم ملؤه في الـ Service إذا كان SourceId موجود
            };
        }

        public static RemembranceListDto ToListDto(this Remembrance remembrance)
        {
            return new RemembranceListDto
            {
                Id = remembrance.Id,
                Title = remembrance.Title,
                Text = remembrance.Text,
                RecommendedCount = remembrance.RecommendedCount,
                Benefits = remembrance.Benefits,
                IsDaily = remembrance.IsDaily
            };
        }

        // للـ Create
        public static RemembranceCategory ToEntity(this RemembranceCategoryCreateDto dto)
        {
            return new RemembranceCategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                NameAr = dto.NameAr,
                NameEn = dto.NameEn,
                IsDaily = dto.IsDaily,
                CreateAt = DateTime.UtcNow
            };
        }

        public static Remembrance ToEntity(this RemembranceCreateDto dto)
        {
            return new Remembrance
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Text = dto.Text,
                RecommendedCount = dto.RecommendedCount,
                Benefits = dto.Benefits,
                IsDaily = dto.IsDaily,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}


