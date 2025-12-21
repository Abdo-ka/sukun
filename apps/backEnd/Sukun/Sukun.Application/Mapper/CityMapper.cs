using Sukun.Application.Dtos.City.Request;
using Sukun.Application.Dtos.City.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class CityMapper
    {
        public static CityResponseDto ToResponseDto(this City city)
        {
            if (city == null) return null!;

            return new CityResponseDto
            {
                Id = city.Id,
                Name = city.Name,
                NameAr = city.NameAr,
                CountryCode = city.CountryCode,
                Country = city.Country,
                Latitude = city.Latitude,
                Longitude = city.Longitude,
                TimeZone = city.TimeZone,
                CreateAt = city.CreateAt,
                UpdateAt = city.UpdatedAt
            };
        }
        public static CitySimpleDto ToSimpleDto(City city)
        {
            if (city == null) return null!;

            return new CitySimpleDto
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country,
                CountryCode = city.CountryCode
            };
        }
        public static void UpdateFromDto(City city, CityUpdateDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name))
                city.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.NameAr))
                city.NameAr = dto.NameAr;
            if (!string.IsNullOrWhiteSpace(dto.Country))
                city.Country = dto.Country;
            if (dto.Latitude.HasValue)
                city.Latitude = dto.Latitude.Value;
            if (dto.Longitude.HasValue)
                city.Longitude = dto.Longitude.Value;
            if (dto.TimeZone.HasValue)
                city.TimeZone = dto.TimeZone.Value;

            city.UpdatedAt = DateTime.UtcNow;
        }
        public static City FromCreateDto(CityCreateDto dto)
        {
            return new City
            {
                Name = dto.Name,
                NameAr = dto.NameAr,
                CountryCode = dto.CountryCode,
                Country = dto.Country,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                TimeZone = dto.TimeZone,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}
