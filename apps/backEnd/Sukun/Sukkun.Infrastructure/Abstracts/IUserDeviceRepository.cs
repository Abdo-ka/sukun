using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IUserDeviceRepository : IRepository<UserDevice>
    {
        Task<UserDevice?> GetDeviceByDeviceIdAsync(string deviceId);
        Task<IEnumerable<UserDevice>> GetDevicesByUserAsync(Guid userId);
        Task<IEnumerable<UserDevice>> GetActiveDevicesByUserAsync(Guid userId);
        Task<UserDevice?> GetUserDeviceAsync(Guid userId, string deviceId);
        Task<bool> DeviceExistsAsync(Guid userId, string deviceId);
        Task<Result<UserDevice>> UpdateLastActiveAsync(Guid deviceId);
        Task<Result<UserDevice>> EnableNotificationsAsync(Guid deviceId);
        Task<Result<UserDevice>> DisableNotificationsAsync(Guid deviceId);
        Task<int> CountDevicesByUserAsync(Guid userId);
        Task<IEnumerable<UserDevice>> GetInactiveDevicesAsync(DateTime cutoffDate);
        Task<Result<int>> CleanupInactiveDevicesAsync(DateTime cutoffDate);
    }
}
