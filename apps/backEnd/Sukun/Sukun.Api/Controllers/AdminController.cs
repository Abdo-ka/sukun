using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Admin.Request;
using Sukun.Application.Dtos.Admin.Response;
using Sukun.Application.Interfaces;
using Sukun.Domin.Common;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ApiResult<AdminLoginResponseDto>> Login([FromBody] AdminLoginDto request)
        {
            _logger.LogInformation("Admin login attempt with email: {Email}", request.Email);
            var result = await _adminService.LoginAsync(request);
            return this.ToApiResult(result);
        }


        [HttpPost]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ApiResult<AdminResponseDto>> CreateAdmin([FromForm] AdminCreateDto request)
        {
            _logger.LogInformation("Creating new admin with email: {Email}", request.Email);
            var result = await _adminService.CreateAdminAsync(request);
            return this.ToApiResult(result);
        }


        [HttpGet("me")]
        [Authorize]
        public async Task<ApiResult<AdminResponseDto>> GetCurrentAdmin()
        {
            var adminIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(adminIdClaim, out var adminId))
                return this.ToApiResult(Result<AdminResponseDto>.Failure("Invalid token"));

            var result = await _adminService.GetAdminByIdAsync(adminId);
            return this.ToApiResult(result);
        }

        [HttpGet("{adminId:guid}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ApiResult<AdminResponseDto>> GetAdminById(Guid adminId)
        {
            // يمكن إضافة تحقق إضافي: إذا كان Admin عادي، يجب أن يكون adminId = نفسه
            var result = await _adminService.GetAdminByIdAsync(adminId);
            return this.ToApiResult(result);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ApiResult<IEnumerable<AdminResponseDto>>> GetAllAdmins()
        {
            var result = await _adminService.GetAllAdminsAsync();
            return this.ToApiResult(result);
        }


        [HttpPut("{adminId:guid}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ApiResult<AdminResponseDto>> UpdateAdmin(Guid adminId, [FromBody] AdminUpdateDto request)
        {
            // تحقق إضافي: Admin عادي لا يعدل غير نفسه
            var currentAdminId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
            var currentRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (currentRole != "SuperAdmin" && currentAdminId != adminId)
                return this.ToApiResult(Result<AdminResponseDto>.Failure("Unauthorized to update this admin"));

            HttpContext.Items["TargetAdminId"] = adminId;
            var result = await _adminService.UpdateAdminAsync(adminId, request);
            return this.ToApiResult(result);
        }


        [HttpDelete("{adminId:guid}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ApiResult<bool>> DeleteAdmin(Guid adminId)
        {
            var currentAdminId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
            if (currentAdminId == adminId)
                return this.ToApiResult<bool>(Result<bool>.Failure("Cannot delete your own account"));

            var result = await _adminService.DeleteAdminAsync(adminId);
            return this.ToApiResult(result);
        }


        [HttpPatch("{adminId:guid}/change-password")]
        [Authorize]
        public async Task<ApiResult<AdminResponseDto>> ChangePassword(Guid adminId, [FromBody] AdminChangePasswordDto request)
        {
            var currentAdminId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
            if (currentAdminId != adminId)
                return this.ToApiResult(Result<AdminResponseDto>.Failure("You can only change your own password"));

            var result = await _adminService.ChangePasswordAsync(adminId, request);
            return this.ToApiResult(result);
        }
    }

}
