using Sukun.Application.Dtos.UserDevice.Request;
using Sukun.Application.Dtos.UserDevice.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class UserDeviceMapper
    {
        public static UserDeviceResponseDto ToResponseDto(UserDevice device)
        {
            return new UserDeviceResponseDto
            {
                Id = device.Id,
                UserId = device.UserId,
                DeviceId = device.DeviceId,
                DeviceType = device.DeviceType,
                DeviceModel = device.DeviceModel,
                IsNotificationsEnabled = device.IsNotificationsEnabled,
                LastNotificationSent = device.LastNotificationSent,
                LastActiveDate = device.LastActiveDate,
                AppVersion = device.AppVersion,
                CreateAt = device.CreateAt,
                UpdateAt = device.UpdatedAt
            };
        }
        public static UserDevice FromCreateDto(UserDeviceCreateDto dto, Guid userId)
        {
            return new UserDevice
            {
                UserId = userId,
                DeviceId = dto.DeviceId,
                DeviceType = dto.DeviceType,
                DeviceModel = dto.DeviceModel,
                AppVersion = dto.AppVersion,
                IsNotificationsEnabled = dto.IsNotificationsEnabled,
                LastActiveDate = DateTime.UtcNow,
                CreateAt = DateTime.UtcNow
            };
        }

        public static void UpdateFromDto(UserDevice device, UserDeviceUpdateDto dto)
        {
            if (dto.IsNotificationsEnabled.HasValue)
                device.IsNotificationsEnabled = dto.IsNotificationsEnabled.Value;

            if (!string.IsNullOrWhiteSpace(dto.DeviceModel))
                device.DeviceModel = dto.DeviceModel;

            if (!string.IsNullOrWhiteSpace(dto.AppVersion))
                device.AppVersion = dto.AppVersion;

            device.UpdatedAt = DateTime.UtcNow;
        }
    }
}
