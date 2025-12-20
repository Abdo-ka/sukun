namespace Sukun.Application.Dtos.FCMToken.Response
{
    // FCMToken DTOs
    public class FCMTokenResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserDeviceId { get; set; }
        public string Token { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

