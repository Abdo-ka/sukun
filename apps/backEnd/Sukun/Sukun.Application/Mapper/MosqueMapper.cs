using Sukun.Application.Dtos.Mosque.Request;
using Sukun.Application.Dtos.Mosque.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class MosqueMapper
    {
        public static MosqueResponseDto ToResponseDto(this Mosque mosque)
        {
            return new MosqueResponseDto
            {
                Id = mosque.Id,
                Name = mosque.Name,
                NameAr = mosque.NameAr,
                Address = mosque.Address,
                Latitude = mosque.Latitude,
                Longitude = mosque.Longitude,
                PhoneNumber = mosque.PhoneNumber,
                Website = mosque.Website,
                Email = mosque.Email,
                HasPrayerFacilities = mosque.HasPrayerFacilities,
                HasWomenSection = mosque.HasWomenSection,
                HasParking = mosque.HasParking,
                IsJummahMasjid = mosque.IsJummahMasjid,
                IsVerified = mosque.IsVerified,
                Rating = mosque.Rating,
                ReviewCount = mosque.ReviewCount,
                CityId = mosque.CityId,
                CityName = mosque.City?.Name ?? string.Empty
            };
        }

        public static Mosque ToEntity(this MosqueCreateDto dto)
        {
            return new Mosque
            {
                Id = Guid.NewGuid(),
                CityId = dto.CityId,
                Name = dto.Name,
                NameAr = dto.NameAr,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                PhoneNumber = dto.PhoneNumber,
                Website = dto.Website,
                Email = dto.Email,
                HasPrayerFacilities = dto.HasPrayerFacilities,
                HasWomenSection = dto.HasWomenSection,
                HasParking = dto.HasParking,
                IsJummahMasjid = dto.IsJummahMasjid,
                IsVerified = dto.IsVerified,
                CreateAt = DateTime.UtcNow
            };
        }
    }
}


