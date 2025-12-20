namespace Sukun.Application.Dtos.FCMToken.Request
{
    public class FCMTokenCreateDto
    {
        public Guid UserDeviceId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}

