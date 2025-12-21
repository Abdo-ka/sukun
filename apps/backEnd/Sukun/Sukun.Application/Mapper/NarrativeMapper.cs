using Sukun.Application.Dtos.City.Request;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class NarrativeMapper
    {
        public static NarrativeListResponseDto ToListDto(this Narrative entity)
        {
            return new NarrativeListResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                TitleAr = entity.TitleAr,
                Type = entity.Type,
                ShortDescription = entity.ShortDescription,
                ParentId = entity.ParentId ,
                CoverImageUrl = entity.CoverImageUrl,
                IsFeatured = entity.IsFeatured,
                ViewCount = entity.ViewCount
            };
        }

        public static NarrativeResponseDto ToResponseDto(this Narrative entity)
        {
            return new NarrativeResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                TitleAr = entity.TitleAr,
                Type = entity.Type,
                ShortDescription = entity.ShortDescription,
                ParentId = entity.ParentId,
                CoverImageUrl = entity.CoverImageUrl,
                IsFeatured = entity.IsFeatured,
                ViewCount = entity.ViewCount,
                Sections = entity.Sections
                    .OrderBy(s => s.DisplayOrder)
                    .Select(s => s.ToSectionDto())
                    .ToList(),
                Children = entity.Children.Select(ToResponseDto) // أو ToResponseDto إذا أردت تفاصيل الأبناء
            };
        }

        public static NarrativeResponseDto ToResponseDtoWithChildren(this Narrative entity)
        {
            return new NarrativeResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                TitleAr = entity.TitleAr,
                Type = entity.Type,
                ShortDescription = entity.ShortDescription,
                ParentId = entity.ParentId,
                CoverImageUrl = entity.CoverImageUrl,
                IsFeatured = entity.IsFeatured,
                ViewCount = entity.ViewCount,
                Sections = entity.Sections
                    .OrderBy(s => s.DisplayOrder)
                    .Select(s => s.ToSectionDto())
                    .ToList(),
                Children = entity.Children.Select(c => c.ToResponseDtoWithChildren()).ToList()
            };
        }

        public static NarrativeSectionResponseDto ToSectionDto(this NarrativeSection section)
        {
            return new NarrativeSectionResponseDto
            {
                Id = section.Id,
                Title = section.Title,
                Content = section.Content,
                DisplayOrder = section.DisplayOrder,
                MediaUrl = section.MediaUrl
            };
        }
        public static NarrativeSection FromCreateSectionDto(this NarrativeSectionCreateDto sectionDto)
        {
            return new NarrativeSection
            {
                Title = sectionDto.Title,
                Content = sectionDto.Content,
                DisplayOrder = sectionDto.DisplayOrder,
                MediaUrl = sectionDto.MediaUrl
            };
        }
        public static Narrative FromCreateDto(this NarrativeCreateDto dto)
        {
            return new Narrative
            {
                Title = dto.Title,
                TitleAr = dto.TitleAr,
                Type = dto.Type,
                ShortDescription = dto.ShortDescription,
                ParentId = dto.ParentId,
                CoverImageUrl = dto.CoverImageUrl,
                IsFeatured = dto.IsFeatured,
                Sections = dto.Sections
                    .OrderBy(s => s.DisplayOrder)
                    .Select(s => s.FromCreateSectionDto())
                    .ToList()
            };
        }
    }
}
