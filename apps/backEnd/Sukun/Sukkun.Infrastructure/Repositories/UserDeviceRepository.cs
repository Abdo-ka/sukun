using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class UserDeviceRepository : Repository<UserDevice>, IUserDeviceRepository
    {
        public UserDeviceRepository(ApplicationDbContext context, ILogger<UserDeviceRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<UserDevice?> GetDeviceByDeviceIdAsync(string deviceId)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting device by device ID: {DeviceId}", deviceId);
                throw;
            }
        }

        public async Task<IEnumerable<UserDevice>> GetDevicesByUserAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Where(d => d.UserId == userId)
                    .OrderByDescending(d => d.LastActiveDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting devices by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<UserDevice>> GetActiveDevicesByUserAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Where(d => d.UserId == userId && d.LastActiveDate > DateTime.UtcNow.AddDays(-30))
                    .OrderByDescending(d => d.LastActiveDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active devices by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<UserDevice?> GetUserDeviceAsync(Guid userId, string deviceId)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(d => d.UserId == userId && d.DeviceId == deviceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user device: {UserId}, {DeviceId}", userId, deviceId);
                throw;
            }
        }

        public async Task<bool> DeviceExistsAsync(Guid userId, string deviceId)
        {
            try
            {
                return await _dbSet
                    .AnyAsync(d => d.UserId == userId && d.DeviceId == deviceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking device existence: {UserId}, {DeviceId}", userId, deviceId);
                throw;
            }
        }

        public async Task<Result<UserDevice>> UpdateLastActiveAsync(Guid deviceId)
        {
            try
            {
                var device = await GetByIdAsync(deviceId);
                if (device == null)
                    return Result<UserDevice>.Failure($"Device with ID {deviceId} not found");

                device.LastActiveDate = DateTime.UtcNow;
                device.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(device);
                await _context.SaveChangesAsync();

                return Result<UserDevice>.Success(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last active for device: {DeviceId}", deviceId);
                return Result<UserDevice>.Failure(ex.Message);
            }
        }

        public async Task<Result<UserDevice>> EnableNotificationsAsync(Guid deviceId)
        {
            try
            {
                var device = await GetByIdAsync(deviceId);
                if (device == null)
                    return Result<UserDevice>.Failure($"Device with ID {deviceId} not found");

                device.IsNotificationsEnabled = true;
                device.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(device);
                await _context.SaveChangesAsync();

                return Result<UserDevice>.Success(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enabling notifications for device: {DeviceId}", deviceId);
                return Result<UserDevice>.Failure(ex.Message);
            }
        }

        public async Task<Result<UserDevice>> DisableNotificationsAsync(Guid deviceId)
        {
            try
            {
                var device = await GetByIdAsync(deviceId);
                if (device == null)
                    return Result<UserDevice>.Failure($"Device with ID {deviceId} not found");

                device.IsNotificationsEnabled = false;
                device.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(device);
                await _context.SaveChangesAsync();

                return Result<UserDevice>.Success(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disabling notifications for device: {DeviceId}", deviceId);
                return Result<UserDevice>.Failure(ex.Message);
            }
        }

        public async Task<int> CountDevicesByUserAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Where(d => d.UserId == userId)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting devices by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<UserDevice>> GetInactiveDevicesAsync(DateTime cutoffDate)
        {
            try
            {
                return await _dbSet
                    .Where(d => d.LastActiveDate < cutoffDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting inactive devices before: {CutoffDate}", cutoffDate);
                throw;
            }
        }

        public async Task<Result<int>> CleanupInactiveDevicesAsync(DateTime cutoffDate)
        {
            try
            {
                var inactiveDevices = await GetInactiveDevicesAsync(cutoffDate);

                if (!inactiveDevices.Any())
                    return Result<int>.Success(0);

                var result = await DeleteRangeAsync(inactiveDevices);
                return result.IsSuccess
                    ? Result<int>.Success(inactiveDevices.Count())
                    : Result<int>.Failure(result.Message!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up inactive devices before: {CutoffDate}", cutoffDate);
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
