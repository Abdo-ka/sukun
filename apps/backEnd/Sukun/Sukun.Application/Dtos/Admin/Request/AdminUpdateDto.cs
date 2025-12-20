using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Admin.Request
{
    public class AdminUpdateDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public AdminRole? Role { get; set; }
        public bool? IsActive { get; set; }
    }
}

