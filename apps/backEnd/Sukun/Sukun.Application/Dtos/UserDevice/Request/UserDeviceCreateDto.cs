namespace Sukun.Application.Dtos.UserDevice.Request
{
    // UserDevice
    public class UserDeviceCreateDto
    {
        public string DeviceId { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty; // iOS, Android, Web
        public string DeviceModel { get; set; } = string.Empty;
        public string? AppVersion { get; set; }
        public bool IsNotificationsEnabled { get; set; } = true;
    }
}

