using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.UserDevice.Response;

namespace Sukun.Application.Dtos.User.Responce
{
    public class UserDetailsResponseDto : UserResponseDto
    {
        public IEnumerable<UserDeviceResponseDto> Devices { get; set; } = new List<UserDeviceResponseDto>();
        public IEnumerable<UserBookmarkResponseDto> Bookmarks { get; set; } = new List<UserBookmarkResponseDto>();
    }
}

