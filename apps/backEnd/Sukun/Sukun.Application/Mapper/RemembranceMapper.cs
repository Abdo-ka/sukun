using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Application.Dtos.RemembranceCategory.Request;
using Sukun.Application.Dtos.RemembranceCategory.Response;
using Sukun.Application.Dtos.RemembranceContent.Response;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

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
                NameEn = category.NameEn,
                RemembrancesCount = category.RemembranceCategoryLinks.Count(r => !r.IsDeleted)
            };
        }
        public static List<RemembranceCategoryResponseDto> ToResponseDtoList(this IEnumerable<RemembranceCategory> entities)
        {
            return entities?.Select(ToResponseDto).Where(dto => dto != null).ToList() ?? new List<RemembranceCategoryResponseDto>();
        }

        public static RemembranceCategoryWithRemembrancesDto ToCategoryWithRemembrancesDto(this RemembranceCategory category)
        {
            return new RemembranceCategoryWithRemembrancesDto
            {
                Id = category.Id,
                Name = category.Name,
                NameEn = category.NameEn,
                Remembrances = category.RemembranceCategoryLinks
                    .Where(r => !r.IsDeleted)
                    .Select(r => r.Remembrance.ToResponseDto())
                    .ToList()
            };
        }
        public static RemembranceCategoryWithRemembrancesDto ToCategoryWithRemembrancesDto(this RemembranceCategory category , IEnumerable<Remembrance> remembrances, Dictionary<Guid, SourcePreviewDto> sourcePreviews)
        {
            return new RemembranceCategoryWithRemembrancesDto
            {
                Id = category.Id,
                Name = category.Name,
                NameEn = category.NameEn,
                Remembrances = remembrances.ToResponseDtoList(sourcePreviews)
            };
        }
        public static RemembranceCategory ToEntity(this RemembranceCategoryCreateDto dto)
        {
            return new RemembranceCategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                NameEn = dto.NameEn,
                CreateAt = DateTime.UtcNow
            };
        }
      
        public static RemembranceResponseDto ToResponseDto(this Remembrance remembrance)
        {
            return new RemembranceResponseDto
            {
                Id = remembrance.Id,
                Title = remembrance.Title,
                RecommendedCount = remembrance.RecommendedCount,
                Benefits = remembrance.Benefits,
                Categories = remembrance.RemembranceCategoryLinks
                .Select(l => l.RemembranceCategory.ToResponseDto())
                .Where(c => c != null)
                .ToList() ?? new List<RemembranceCategoryResponseDto>(),
                Contents = new List<RemembranceContentResponseDto>(),
            };
        }
        public static RemembranceResponseDto ToResponseDto(this Remembrance remembrance, Dictionary<Guid, SourcePreviewDto> sourcePreviews)
        {
            var dto = remembrance.ToResponseDto();

            if (dto != null)
            {
                dto.Contents = remembrance.Contents?.ToContentResponseDtos(sourcePreviews) ?? new List<RemembranceContentResponseDto>();
            }

            return dto;
        }
        public static List<RemembranceResponseDto> ToResponseDtoList(IEnumerable<Remembrance> entities)
        {
            return entities.Select(ToResponseDto).ToList();
        }

        public static List<RemembranceResponseDto> ToResponseDtoList(
        this IEnumerable<Remembrance> entities,
        Dictionary<Guid, SourcePreviewDto> sourcePreviews)
        {
            return entities?
                .Select(r => r.ToResponseDto(sourcePreviews))
                .Where(dto => dto != null)
                .ToList() ?? new List<RemembranceResponseDto>();
        }
       
        public static RemembranceListDto ToListDto(this Remembrance remembrance)
        {
            return new RemembranceListDto
            {
                Id = remembrance.Id,
                Title = remembrance.Title,
                RecommendedCount = remembrance.RecommendedCount,
                Benefits = remembrance.Benefits,
            };
        }
        public static void UpdateEntity(Remembrance entity, RemembranceUpdateDto dto)
        {
            entity.Title = dto.Title;
            entity.RecommendedCount = dto.RecommendedCount;
            entity.Benefits = dto.Benefits;
        }
        public static Remembrance ToEntity(this RemembranceCreateDto dto)
        {
            return new Remembrance
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                RecommendedCount = dto.RecommendedCount,
                Benefits = dto.Benefits,
                CreateAt = DateTime.UtcNow,
                Contents = dto.Contents.Select(c => new RemembranceContent
                {
                    Id = Guid.NewGuid(),
                    SourceType = c.SourceType,
                    SourceId = c.SourceId,
                    CustomContent = c.CustomContent,
                    RemembranceId = Guid.Empty
                }).ToList(),
                RemembranceCategoryLinks = new List<RemembranceCategoryLinks>()
            };
        }

        public static RemembranceContentResponseDto ToContentDto(this RemembranceContent content)
        {
            return new RemembranceContentResponseDto
            {
                Id = content.Id,
                SourceType = content.SourceType,
                SourceId = content.SourceId,
                CustomContent = content.CustomContent,
            };
        }
        public static RemembranceContentResponseDto ToContentResponseDto( this RemembranceContent content,Dictionary<Guid, SourcePreviewDto> sourcePreviews)
        {
            if (content == null) return null;

            var dto = new RemembranceContentResponseDto
            {
                Id = content.Id,
                SourceType = content.SourceType,
                SourceId = content.SourceId,
                CustomContent = content.CustomContent
            };

            if (content.SourceType == SourceType.Custom)
            {
                dto.DisplayText = content.CustomContent ?? "";
            }
            else if (content.SourceId.HasValue &&
                     sourcePreviews.TryGetValue(content.SourceId.Value, out var preview))
            {
                dto.DisplayText = preview.DisplayText;
                dto.Reference = preview.Reference;
                dto.DisplayTextEn = preview.DisplayTextEn;
                dto.MeaningEn = preview.MeaningEn;
                dto.Meaning = preview.Meaning;
                dto.Benefits = preview.Benefits;
            }
            else
            {
                dto.DisplayText = "[المصدر غير متوفر]";
            }

            return dto;
        }
        public static List<RemembranceContentResponseDto> ToContentResponseDtos( this IEnumerable<RemembranceContent> contents, Dictionary<Guid, SourcePreviewDto> sourcePreviews)
        {
            return contents?
                .Where(c => !c.IsDeleted)
                .Select(c => c.ToContentResponseDto(sourcePreviews))
                .Where(dto => dto != null)
                .ToList() ?? new List<RemembranceContentResponseDto>();
        }
        public static List<RemembranceContentResponseDto> ToResponseDtos( this IEnumerable<RemembranceContent> contents,  Dictionary<Guid, SourcePreviewDto> sourcePreviews)
        {
            return contents
                .Where(c => !c.IsDeleted)
                .Select(c =>
                {
                    var dto = new RemembranceContentResponseDto
                    {
                        Id = c.Id,
                        SourceType = c.SourceType,
                        SourceId = c.SourceId,
                        CustomContent = c.CustomContent
                    };

                    if (c.SourceType == SourceType.Custom)
                    {
                        dto.DisplayText = c.CustomContent ?? "";
                    }
                    else if (c.SourceId.HasValue && sourcePreviews.TryGetValue(c.SourceId.Value, out var preview))
                    {
                        dto.DisplayText = preview.DisplayText;
                        dto.Reference = preview.Reference;
                        dto.DisplayTextEn = preview.DisplayTextEn;
                        dto.MeaningEn = preview.MeaningEn;
                        dto.Meaning = preview.Meaning;
                        dto.Benefits = preview.Benefits;
                    }
                    else
                    {
                        dto.DisplayText = "[المصدر غير متوفر]";
                    }

                    return dto;
                })
                .ToList();
        }
    }
}


