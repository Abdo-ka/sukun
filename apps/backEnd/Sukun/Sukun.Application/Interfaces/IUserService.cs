using Sukun.Application.Dtos.User.Responce;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserResponseDto>> GetOrCreateAnonymousUserAsync(Guid userId);
        Task<Result<UserDetailsResponseDto>> GetUserDetailAsync(Guid userId);
        Task<Result> UpdateUserCityAsync(Guid userId, Guid? cityId);
        Task<Result<bool>> UserExistsAsync(Guid userId);
    }
}