using Sukun.Application.Dtos.Admin.Request;
using Sukun.Application.Dtos.Admin.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IAdminService
    {
        Task<Result<AdminLoginResponseDto>> LoginAsync(AdminLoginDto dto);
        Task<Result<AdminResponseDto>> CreateAdminAsync(AdminCreateDto dto);
        Task<Result<AdminResponseDto>> GetAdminByIdAsync(Guid adminId);
        Task<Result<IEnumerable<AdminResponseDto>>> GetAllAdminsAsync();
        Task<Result<AdminResponseDto>> UpdateAdminAsync(Guid adminId, AdminUpdateDto dto);
        Task<Result<bool>> DeleteAdminAsync(Guid adminId);
        Task<Result<AdminResponseDto>> ChangePasswordAsync(Guid adminId, AdminChangePasswordDto dto);
    }
}