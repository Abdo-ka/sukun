using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.City.Response;
using Sukun.Application.Dtos.User.Responce;
using Sukun.Application.Dtos.UserDevice.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class UserMapper
    {
        public static UserResponseDto ToResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                CityInfo = user.City is not null ? CityMapper.ToResponseDto(user.City) : new CityResponseDto(),
                CreateAt = user.CreateAt,
                UpdateAt = user.UpdatedAt
            };
        }

        public static UserDetailsResponseDto ToDetailDto(User user)
        {
            return new UserDetailsResponseDto
            {
                Id = user.Id,
                CityInfo = CityMapper.ToResponseDto(user.City),
                Devices = user.Devices?.Select(x => UserDeviceMapper.ToResponseDto(x)) ?? new List<UserDeviceResponseDto>(),
                Bookmarks = user.Bookmarks?.Select(x => UserBookmarkMapper.ToResponseDto(x)) ?? new List<UserBookmarkResponseDto>(),
                CreateAt = user.CreateAt,
                UpdateAt = user.UpdatedAt
            };
        }
      
    }
}
