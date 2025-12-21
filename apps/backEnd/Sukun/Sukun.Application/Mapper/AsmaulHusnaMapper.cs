using Sukun.Application.Dtos.AsmaulHusna.Responce;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class AsmaulHusnaMapper
    {
        public static AsmaulHusnaResponseDto ToResponseDto(this AsmaulHusna entity)
        {
            return new AsmaulHusnaResponseDto
            {
                Id = entity.Id,
                Number = entity.Number,
                NameArabic = entity.NameArabic,
                NameTransliteration = entity.NameTransliteration,
                MeaningArabic = entity.MeaningArabic,
                MeaningEnglish = entity.MeaningEnglish
            };
        }

        public static IEnumerable<AsmaulHusnaResponseDto> ToResponseDtos(this IEnumerable<AsmaulHusna> entities)
        {
            return entities.Select(ToResponseDto);
        }
    }
}
