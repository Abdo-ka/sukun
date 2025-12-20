namespace Sukun.Application.Dtos.UserDevice.Request
{
    public class UserDeviceUpdateDto
    {
        public string? DeviceModel { get; set; }
        public string? AppVersion { get; set; }
        public bool? IsNotificationsEnabled { get; set; }
    }
}

