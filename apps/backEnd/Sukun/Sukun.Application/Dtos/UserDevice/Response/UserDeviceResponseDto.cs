namespace Sukun.Application.Dtos.UserDevice.Response
{
    public class UserDeviceResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceModel { get; set; }
        public bool IsNotificationsEnabled { get; set; }
        public DateTime? LastNotificationSent { get; set; }
        public DateTime LastActiveDate { get; set; }
        public string? AppVersion { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

