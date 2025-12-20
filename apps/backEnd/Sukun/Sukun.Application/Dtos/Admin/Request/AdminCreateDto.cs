using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Admin.Request
{
    public class AdminCreateDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public AdminRole Role { get; set; } = AdminRole.Admin;
    }
}

