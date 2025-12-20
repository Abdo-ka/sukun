using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sukun.Application.Dtos.Admin.Request;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.Admin.Validators
{
    public class AdminUpdateDtoValidator : AbstractValidator<AdminUpdateDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminUpdateDtoValidator(IAdminRepository adminRepository, IHttpContextAccessor httpContextAccessor)
        {
            _adminRepository = adminRepository;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(dto => dto)
                .MustAsync(async (dto, cancellation) =>
                {
                    var currentAdminId = GetCurrentAdminId();
                    var currentRole = GetCurrentAdminRole();

                    if (currentRole != AdminRole.SuperAdmin)
                    {
                        var targetAdminId = GetTargetAdminId(); 
                        return currentAdminId == targetAdminId;
                    }
                    return true;
                })
                .WithMessage("You can only update your own account");

            RuleFor(dto => dto.Role)
                .Must(role => role != AdminRole.SuperAdmin || GetCurrentAdminRole() == AdminRole.SuperAdmin)
                .WithMessage("Only SuperAdmin can assign SuperAdmin role");

             RuleFor(dto => dto.Role)
                .Must(role =>
                {
                    var currentAdminId = GetCurrentAdminId();
                    var currentRole = GetCurrentAdminRole();
                    var targetAdminId = GetTargetAdminId();

                    if (currentAdminId == targetAdminId && currentRole == AdminRole.SuperAdmin)
                        return role == null || role == AdminRole.SuperAdmin;

                    return true;
                })
                .WithMessage("SuperAdmin cannot downgrade their own role");

            RuleFor(dto => dto.Email)
                .MustAsync(async (email, cancellation) =>
                {
                    if (string.IsNullOrWhiteSpace(email)) return true;

                    var targetAdminId = GetTargetAdminId();
                    var admin = await _adminRepository.GetByIdAsync(targetAdminId);
                    if (admin == null || email == admin.Email) return true;

                    return await _adminRepository.IsEmailUniqueAsync(email, targetAdminId);
                })
                .WithMessage("Email already exists");
        }

        private Guid GetCurrentAdminId()
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
        }

        private AdminRole GetCurrentAdminRole()
        {
            var roleStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
            return Enum.TryParse<AdminRole>(roleStr, out var role) ? role : AdminRole.Moderator;
        }

        private Guid GetTargetAdminId()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.Items["TargetAdminId"] is Guid targetId)
                return targetId;

            return Guid.Empty;
        }
    }
}
