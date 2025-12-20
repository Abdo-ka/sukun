namespace Sukun.Application.Dtos.Admin.Response
{
    public class AdminLoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public AdminResponseDto Admin { get; set; } = new();
    }
}

