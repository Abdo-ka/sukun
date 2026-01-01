using Sukun.Application.Dtos.NarrativeCategory.Request;
using Sukun.Application.Dtos.NarrativeCategory.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class CategoryMapper
    {
        public static Category ToEntity(this CategoryCreateDto dto)
        {
            return new Category
            {
                Title = dto.Title,
                TitleAr = dto.TitleAr,
                Order = dto.Order,
                IsMainSection = dto.IsMainSection,
                ParentId  = dto.ParentId
            };
        }
        public static CategoryResponseDto ToResponseDto(this Category category)
        {
            return new CategoryResponseDto
            {
                Id = category.Id,
                Title = category.Title,
                TitleAr = category.TitleAr,
                Order = category.Order,
                IsMainSection = category.IsMainSection,
                ParentId = category.ParentId
            };
        }

        public static CategoryWithChildrenResponseDto ToWithChildrenDto(this Category category)
        {
            return new CategoryWithChildrenResponseDto
            {
                Id = category.Id,
                Title = category.Title,
                TitleAr = category.TitleAr,
                Order = category.Order,
                IsMainSection = category.IsMainSection,
                ParentId = category.ParentId,
                Children = category.Children
                    .OrderBy(c => c.Order)
                    .Select(c => c.ToWithChildrenDto())
                    .ToList()
            };
        }
        public static CategoryWithNarrativesResponseDto ToWithNarrativeDto(this Category category)
        {
            return new CategoryWithNarrativesResponseDto
            {
                Id = category.Id,
                Title = category.Title,
                TitleAr = category.TitleAr,
                Order = category.Order,
                IsMainSection = category.IsMainSection,
                ParentId = category.ParentId,
                Narratives = category.NarrativeCategories
                    .Select(c => c.Narrative.ToListDto())
                    .ToList()
            };
        }
    }
}
