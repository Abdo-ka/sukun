using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.User.Responce;
using Sukun.Application.Dtos.UserDevice.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class UserService : BaseService, IUserService
    {
        public IPasswordHasher _passwordHasher { get; }

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IPasswordHasher passwordHasher) : base(unitOfWork, logger)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserResponseDto>> GetOrCreateAnonymousUserAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                    return Result<UserResponseDto>.Failure("Invalid user ID provided");

                var exists = await _unitOfWork.Users.UserExistsAsync(userId);
                User user;

                if (!exists)
                {
                    user = new User { Id = userId };
                    await _unitOfWork.Repository<User>().AddAsync(user);
                    await _unitOfWork.CompleteAsync();
                    _logger.LogInformation("Created new anonymous user: {UserId}", userId);
                }
                else
                {
                    user = await _unitOfWork.Users.GetByIdAsync(userId);
                }

                return Result<UserResponseDto>.Success(new UserResponseDto
                {
                    Id = user!.Id,
                    CityInfo = user.City != null ? CityMapper.ToResponseDto(user.City) : null,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOrCreateAnonymousUser: {UserId}", userId);
                return Result<UserResponseDto>.Failure("Error processing user request");
            }
        }

        public async Task<Result<UserDetailsResponseDto>> GetUserDetailAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                    return Result<UserDetailsResponseDto>.Failure("Invalid user ID");

                var user = await _unitOfWork.Users.GetUserWithDetailsAsync(userId);

                if (user == null)
                    return Result<UserDetailsResponseDto>.NotFound("User not found");

                return Result<UserDetailsResponseDto>.Success(new UserDetailsResponseDto
                {
                    Id = user.Id,
                    CityInfo = user.City != null ? CityMapper.ToResponseDto(user.City) : null,
                    Devices = user.Devices?.Select(UserDeviceMapper.ToResponseDto) ?? new List<UserDeviceResponseDto>(),
                    Bookmarks = user.Bookmarks?.Select(UserBookmarkMapper.ToResponseDto) ?? new List<UserBookmarkResponseDto>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting anonymous user details: {UserId}", userId);
                return Result<UserDetailsResponseDto>.Failure("Error retrieving user details");
            }
        }

        public async Task<Result> UpdateUserCityAsync(Guid userId, Guid? cityId)
        {
            try
            {
                if (userId == Guid.Empty)
                    return Result.Failure("Invalid user ID");

                var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);

                if (user == null)
                {
                    // إنشاء مستخدم جديد مع المدينة مباشرة
                    user = new User { Id = userId, CityId = cityId };
                    await _unitOfWork.Repository<User>().AddAsync(user);
                    _logger.LogInformation("Created anonymous user with city: {UserId}, CityId: {CityId}", userId, cityId);
                }
                else
                {
                    user.CityId = cityId;
                    _logger.LogInformation("Updated city for anonymous user: {UserId} → CityId: {CityId}", userId, cityId);
                }

                await _unitOfWork.CompleteAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating city for user: {UserId}", userId);
                return Result.Failure("Error updating user city");
            }
        }

        public async Task<Result<bool>> UserExistsAsync(Guid userId)
        {
            try
            {
                var exists = await _unitOfWork.Users.UserExistsAsync(userId);
                return Result<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user existence: {UserId}", userId);
                return Result<bool>.Failure("Error checking user");
            }
        }

    }
}