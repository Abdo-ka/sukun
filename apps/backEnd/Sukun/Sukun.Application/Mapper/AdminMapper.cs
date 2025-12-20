using Sukun.Application.Dtos.Admin.Request;
using Sukun.Application.Dtos.Admin.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class AdminMapper
    {
        public static AdminResponseDto ToResponseDto(Admin admin)
        {
            return new AdminResponseDto
            {
                Id = admin.Id,
                Email = admin.Email,
                FullName = admin.FullName,
                Role = admin.Role,
                IsActive = admin.IsActive,
                LastLoginDate = admin.LastLoginDate,
                CreateAt = admin.CreateAt,
                UpdateAt = admin.UpdatedAt
            };
        }

        public static Admin FromCreateDto(AdminCreateDto dto)
        {
            return new Admin
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FullName = dto.FullName,
                Role = dto.Role,
                IsActive = true,
                CreateAt = DateTime.UtcNow
            };
        }

        public static void UpdateFromDto(Admin admin, AdminUpdateDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Email))
                admin.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.FullName))
                admin.FullName = dto.FullName;
            if (dto.Role.HasValue)
                admin.Role = dto.Role.Value;
            if (dto.IsActive.HasValue)
                admin.IsActive = dto.IsActive.Value;

            admin.UpdatedAt = DateTime.UtcNow;
        }
    }
}
