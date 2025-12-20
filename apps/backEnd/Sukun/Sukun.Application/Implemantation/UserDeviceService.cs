using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.UserDevice.Request;
using Sukun.Application.Dtos.UserDevice.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.InfrastructureBases;
using System.Collections.Generic;

namespace Sukun.Application.Implemantation
{
    public class UserDeviceService : BaseService, IUserDeviceService
    {

        public UserDeviceService(IUnitOfWork unitOfWork, ILogger<UserDeviceService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Result<UserDeviceResponseDto>> GetByIdAsync(Guid deviceId)
        {
            try
            {
                _logger.LogDebug("Getting device by ID: {DeviceId}", deviceId);
                var device = await _unitOfWork.UserDevices.GetByIdAsync(deviceId);
                if (device == null)
                    return Result<UserDeviceResponseDto>.NotFound($"Device with ID {deviceId} not found");

                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(device));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting device by ID: {DeviceId}", deviceId);
                return Result<UserDeviceResponseDto>.Failure($"Error retrieving device: {ex.Message}");
            }
        }

        public async Task<Result<UserDeviceResponseDto>> GetDeviceByDeviceIdAsync(string deviceId)
        {
            try
            {

                _logger.LogDebug("Getting device by device ID: {DeviceId}", deviceId);
                var device = await _unitOfWork.UserDevices.GetDeviceByDeviceIdAsync(deviceId);
                if (device == null)
                    return Result<UserDeviceResponseDto>.NotFound($"Device with device ID {deviceId} not found");

                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(device));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting device by device ID: {DeviceId}", deviceId);
                return Result<UserDeviceResponseDto>.Failure($"Error retrieving device: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserDeviceResponseDto>>> GetDevicesByUserAsync(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<IEnumerable<UserDeviceResponseDto>>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting devices by user: {UserId}", userId);
                var devices = await _unitOfWork.UserDevices.GetDevicesByUserAsync(userId);
                return Result<IEnumerable<UserDeviceResponseDto>>.Success(devices.Select(UserDeviceMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting devices by user: {UserId}", userId);
                return Result<IEnumerable<UserDeviceResponseDto>>.Failure($"Error retrieving devices: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserDeviceResponseDto>>> GetActiveDevicesByUserAsync(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<IEnumerable<UserDeviceResponseDto>>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting active devices by user: {UserId}", userId);
                var devices = await _unitOfWork.UserDevices.GetActiveDevicesByUserAsync(userId);
                return Result<IEnumerable<UserDeviceResponseDto>>.Success(devices.Select(UserDeviceMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active devices by user: {UserId}", userId);
                return Result<IEnumerable<UserDeviceResponseDto>>.Failure($"Error retrieving devices: {ex.Message}");
            }
        }

        public async Task<Result<UserDeviceResponseDto>> GetUserDeviceAsync(Guid userId, string deviceId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<UserDeviceResponseDto>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Getting user device: {UserId}, {DeviceId}", userId, deviceId);
                var device = await _unitOfWork.UserDevices.GetUserDeviceAsync(userId, deviceId);
                if (device == null)
                    return Result<UserDeviceResponseDto>.NotFound($"Device not found for user {userId} and device ID {deviceId}");

                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(device));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user device");
                return Result<UserDeviceResponseDto>.Failure($"Error retrieving device: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeviceExistsAsync(Guid userId, string deviceId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<bool>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Checking device existence: {UserId}, {DeviceId}", userId, deviceId);
                var exists = await _unitOfWork.UserDevices.DeviceExistsAsync(userId, deviceId);
                return Result<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking device existence");
                return Result<bool>.Failure($"Error checking device: {ex.Message}");
            }
        }

        public async Task<Result<UserDeviceResponseDto>> UpdateLastActiveAsync(Guid deviceId)
        {
            try
            {
                _logger.LogDebug("Updating last active for device: {DeviceId}", deviceId);
                var result = await _unitOfWork.UserDevices.UpdateLastActiveAsync(deviceId);
                if (!result.IsSuccess)
                    return Result<UserDeviceResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last active for device: {DeviceId}", deviceId);
                return Result<UserDeviceResponseDto>.Failure($"Error updating device: {ex.Message}");
            }
        }

        public async Task<Result<UserDeviceResponseDto>> EnableNotificationsAsync(Guid deviceId)
        {
            try
            {
                _logger.LogDebug("Enabling notifications for device: {DeviceId}", deviceId);
                var result = await _unitOfWork.UserDevices.EnableNotificationsAsync(deviceId);
                if (!result.IsSuccess)
                    return Result<UserDeviceResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enabling notifications for device: {DeviceId}", deviceId);
                return Result<UserDeviceResponseDto>.Failure($"Error updating device: {ex.Message}");
            }
        }

        public async Task<Result<UserDeviceResponseDto>> DisableNotificationsAsync(Guid deviceId)
        {
            try
            {
                _logger.LogDebug("Disabling notifications for device: {DeviceId}", deviceId);
                var result = await _unitOfWork.UserDevices.DisableNotificationsAsync(deviceId);
                if (!result.IsSuccess)
                    return Result<UserDeviceResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disabling notifications for device: {DeviceId}", deviceId);
                return Result<UserDeviceResponseDto>.Failure($"Error updating device: {ex.Message}");
            }
        }

        public async Task<Result<int>> CountDevicesByUserAsync(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<int>.NotFound($"Not found User by id : {userId}");
                _logger.LogDebug("Counting devices by user: {UserId}", userId);
                var count = await _unitOfWork.UserDevices.CountDevicesByUserAsync(userId);
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting devices by user: {UserId}", userId);
                return Result<int>.Failure($"Error counting devices: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<UserDeviceResponseDto>>> GetInactiveDevicesAsync(DateTime cutoffDate)
        {
            try
            {
                _logger.LogDebug("Getting inactive devices before: {CutoffDate}", cutoffDate);
                var devices = await _unitOfWork.UserDevices.GetInactiveDevicesAsync(cutoffDate);
                return Result<IEnumerable<UserDeviceResponseDto>>.Success(devices.Select(UserDeviceMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting inactive devices");
                return Result<IEnumerable<UserDeviceResponseDto>>.Failure($"Error retrieving devices: {ex.Message}");
            }
        }

        public async Task<Result<int>> CleanupInactiveDevicesAsync(DateTime cutoffDate)
        {
            try
            {
                _logger.LogDebug("Cleaning up inactive devices before: {CutoffDate}", cutoffDate);
                var result = await _unitOfWork.UserDevices.CleanupInactiveDevicesAsync(cutoffDate);
                await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up inactive devices");
                return Result<int>.Failure($"Error cleaning up devices: {ex.Message}");
            }
        }
        public async Task<Result<UserDeviceResponseDto>> CreateDeviceAsync(Guid userId, UserDeviceCreateDto dto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByIdAsync(userId);
                if (user == null)
                    return Result<UserDeviceResponseDto>.NotFound($"Not found User by id : {userId}");
                _logger.LogInformation("Creating new device for user: {UserId}", userId);
                var existingDevice = await _unitOfWork.UserDevices.GetUserDeviceAsync(userId, dto.DeviceId);
                if (existingDevice != null)
                {
                    UserDeviceMapper.UpdateFromDto(existingDevice, new UserDeviceUpdateDto
                    {
                        DeviceModel = dto.DeviceModel,
                        AppVersion = dto.AppVersion,
                        IsNotificationsEnabled = dto.IsNotificationsEnabled
                    });
                    existingDevice.LastActiveDate = DateTime.UtcNow;
                    _unitOfWork.UserDevices.UpdateAsync(existingDevice);
                    await _unitOfWork.CompleteAsync();
                    return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(existingDevice));
                }

                var device = UserDeviceMapper.FromCreateDto(dto, userId);
                await _unitOfWork.UserDevices.AddAsync(device);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Device created successfully with ID: {DeviceId}", device.Id);
                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(device));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating device for user: {UserId}", userId);
                return Result<UserDeviceResponseDto>.Failure($"Error creating device: {ex.Message}");
            }
        }

        public async Task<Result<UserDeviceResponseDto>> UpdateDeviceAsync(Guid deviceId, UserDeviceUpdateDto dto)
        {
            try
            {
                _logger.LogInformation("Updating device with ID: {DeviceId}", deviceId);
                var device = await _unitOfWork.UserDevices.GetByIdAsync(deviceId);
                if (device == null)
                    return Result<UserDeviceResponseDto>.NotFound($"Device with ID {deviceId} not found");

                UserDeviceMapper.UpdateFromDto(device, dto);
                await _unitOfWork.UserDevices.UpdateAsync(device);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Device updated successfully with ID: {DeviceId}", deviceId);
                return Result<UserDeviceResponseDto>.Success(UserDeviceMapper.ToResponseDto(device));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating device with ID: {DeviceId}", deviceId);
                return Result<UserDeviceResponseDto>.Failure($"Error updating device: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteDeviceAsync(Guid deviceId)
        {
            try
            {
                _logger.LogInformation("Deleting device with ID: {DeviceId}", deviceId);
                var device = await _unitOfWork.UserDevices.GetByIdAsync(deviceId);
                if (device == null)
                    return Result<bool>.NotFound($"Device with ID {deviceId} not found");

                await _unitOfWork.FCMTokens.DeactivateAllTokensForDeviceAsync(deviceId);
                await _unitOfWork.UserDevices.DeleteAsync(device);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Device deleted successfully with ID: {DeviceId}", deviceId);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting device with ID: {DeviceId}", deviceId);
                return Result<bool>.Failure($"Error deleting device: {ex.Message}");
            }
        }
    }
}