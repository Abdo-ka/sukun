namespace Sukun.Domin.Entities
{
    public class UserDevice : BaseEntity
    {
        public Guid UserId { get; set; }
        // Device Info
        public string DeviceId { get; set; }
        public string DeviceType { get; set; } // iOS, Android, Web
        public string DeviceModel { get; set; }

        // Push Notifications
        public bool IsNotificationsEnabled { get; set; }
        public DateTime? LastNotificationSent { get; set; }

        // Tracking
        public DateTime LastActiveDate { get; set; }
        public string? AppVersion { get; set; }

        // Navigation Property
        public virtual User User { get; set; }
        public ICollection<FCMToken> FCMTokens { get; set; }= new List<FCMToken>();
    }
}




