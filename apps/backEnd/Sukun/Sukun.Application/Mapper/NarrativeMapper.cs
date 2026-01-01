using Sukun.Application.Dtos.City.Request;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Application.Dtos.NarrativeSection.Response;
using Sukun.Application.Dtos.Tag.Response;
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
                IsFeatured = entity.IsFeatured,
                ViewCount = entity.ViewCount,
                Sections = entity.Sections
                    .OrderBy(s => s.DisplayOrder)
                    .Select(s => s.ToSectionDto())
                    .ToList(),
                Tags= entity.NarrativeTags.Select(t => new TagResponseDto
                {
                    Id=t.TagId,
                    NameAr = t.Tag.NameAr,
                    Name = t.Tag.Name
                }).ToList(),
                Categories = entity.NarrativeCategories
                .Select(nc => nc.Category.ToResponseDto())
                .ToList()
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
            };
        }
        public static NarrativeSection FromCreateSectionDto(this NarrativeSectionCreateDto sectionDto)
        {
            return new NarrativeSection
            {
                Title = sectionDto.Title,
                Content = sectionDto.Content,
                DisplayOrder = sectionDto.DisplayOrder,
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
                IsFeatured = dto.IsFeatured,
                Sections = dto.Sections
                    .OrderBy(s => s.DisplayOrder)
                    .Select(s => s.FromCreateSectionDto())
                    .ToList()
            };
        }
    }
}
