using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Application.Dtos.NarrativeSection.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class NarrativeSectionMapper
    {
        public static NarrativeSectionResponseDto ToResponseDto(this NarrativeSection entity)
        {
            return new NarrativeSectionResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                DisplayOrder = entity.DisplayOrder,
            };
        }

        public static NarrativeSection ToEntity(this NarrativeSectionCreateDto dto, Guid narrativeId)
        {
            return new NarrativeSection
            {
                NarrativeId = narrativeId,
                Title = dto.Title,
                Content = dto.Content,
                DisplayOrder = dto.DisplayOrder,
            };
        }

        public static void UpdateFromDto(this NarrativeSection entity, NarrativeSectionUpdateDto dto)
        {
            entity.Title = dto.Title;
            entity.Content = dto.Content;
            entity.DisplayOrder = dto.DisplayOrder.Value;
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}
