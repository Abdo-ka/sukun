using Sukun.Application.Dtos.Tasbih.Request;
using Sukun.Application.Dtos.Tasbih.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class TasbihMapper
    {
        public static TasbihResponseDto ToResponseDto(this Tasbih tasbih)
        {
            return new TasbihResponseDto
            {
                Id = tasbih.Id,
                Title = tasbih.Title,
                TitleAr = tasbih.TitleAr,
                Benefits = tasbih.Benefits,
                RecommendedCount = tasbih.RecommendedCount,
                Order = tasbih.Order
            };
        }

        public static Tasbih ToEntity(this TasbihCreateDto dto)
        {
            return new Tasbih
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                TitleAr = dto.TitleAr,
                Benefits = dto.Benefits,
                RecommendedCount = dto.RecommendedCount,
                Order = dto.Order,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}


