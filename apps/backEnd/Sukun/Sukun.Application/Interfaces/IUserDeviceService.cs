using Sukun.Application.Dtos.UserDevice.Request;
using Sukun.Application.Dtos.UserDevice.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IUserDeviceService
    {
        Task<Result<UserDeviceResponseDto>> GetByIdAsync(Guid deviceId);
        Task<Result<UserDeviceResponseDto>> GetDeviceByDeviceIdAsync(string deviceId);
        Task<Result<IEnumerable<UserDeviceResponseDto>>> GetDevicesByUserAsync(Guid userId);
        Task<Result<IEnumerable<UserDeviceResponseDto>>> GetActiveDevicesByUserAsync(Guid userId);
        Task<Result<UserDeviceResponseDto>> GetUserDeviceAsync(Guid userId, string deviceId);
        Task<Result<bool>> DeviceExistsAsync(Guid userId, string deviceId);
        Task<Result<UserDeviceResponseDto>> UpdateLastActiveAsync(Guid deviceId);
        Task<Result<UserDeviceResponseDto>> EnableNotificationsAsync(Guid deviceId);
        Task<Result<UserDeviceResponseDto>> DisableNotificationsAsync(Guid deviceId);
        Task<Result<int>> CountDevicesByUserAsync(Guid userId);
        Task<Result<IEnumerable<UserDeviceResponseDto>>> GetInactiveDevicesAsync(DateTime cutoffDate);
        Task<Result<int>> CleanupInactiveDevicesAsync(DateTime cutoffDate);
        Task<Result<UserDeviceResponseDto>> CreateDeviceAsync(Guid userId, UserDeviceCreateDto dto);
        Task<Result<UserDeviceResponseDto>> UpdateDeviceAsync(Guid deviceId, UserDeviceUpdateDto dto);
        Task<Result<bool>> DeleteDeviceAsync(Guid deviceId);
    }
}