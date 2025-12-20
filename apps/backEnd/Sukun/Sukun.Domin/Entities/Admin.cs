using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Admin : BaseEntity
    {
        public string Email { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChange { get; set; }

        public AdminRole Role { get; set; } = AdminRole.Admin;
    }
}




