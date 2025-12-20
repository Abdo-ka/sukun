using Sukun.Application.Dtos.City.Response;

namespace Sukun.Application.Dtos.User.Responce
{
    // User DTOs
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public CityResponseDto? CityInfo { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}

