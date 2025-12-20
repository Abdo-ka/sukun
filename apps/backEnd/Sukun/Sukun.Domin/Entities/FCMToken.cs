namespace Sukun.Domin.Entities
{
    public class FCMToken : BaseEntity
    {
        public Guid UserDeviceId { get; set; }
        public string Token { get; set; }

        public DateTime? LastUsedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual UserDevice Device { get; set; }
    }
}




