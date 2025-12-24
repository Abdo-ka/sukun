using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Admin.Request;
using Sukun.Application.Dtos.Admin.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AdminService(
            IUnitOfWork unitOfWork,
            ILogger<AdminService> logger,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
            : base(unitOfWork, logger)
        {
            _adminRepository = unitOfWork.Admins;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<Result<AdminLoginResponseDto>> LoginAsync(AdminLoginDto dto)
        {
            try
            {
                var admin = await _adminRepository.GetByEmailAsync(dto.Email);
                if (admin == null || !admin.IsActive)
                    return Result<AdminLoginResponseDto>.Failure("Invalid email or account is inactive");

                if (!_passwordHasher.VerifyPassword(dto.Password, admin.PasswordHash))
                    return Result<AdminLoginResponseDto>.Failure("Invalid password");

                admin.LastLoginDate = DateTime.UtcNow;
                await _unitOfWork.CompleteAsync();

                var token = _jwtService.GenerateToken(admin.Id, admin.Email, admin.Role.ToString());

                return Result<AdminLoginResponseDto>.Success(new AdminLoginResponseDto
                {
                    Token = token,
                    Admin = AdminMapper.ToResponseDto(admin)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during admin login: {Email}", dto.Email);
                return Result<AdminLoginResponseDto>.Failure("Login failed");
            }
        }

        public async Task<Result<AdminResponseDto>> CreateAdminAsync(AdminCreateDto dto)
        {
            try
            {
                var emailUnique = await _adminRepository.IsEmailUniqueAsync(dto.Email);
                if (!emailUnique)
                    return Result<AdminResponseDto>.Failure("Email already exists");

                var admin = AdminMapper.FromCreateDto(dto);
                admin.PasswordHash = _passwordHasher.HashPassword(dto.Password);

                await _adminRepository.AddAsync(admin);
                await _unitOfWork.CompleteAsync();

                return Result<AdminResponseDto>.Success(AdminMapper.ToResponseDto(admin));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating admin: {Email}", dto.Email);
                return Result<AdminResponseDto>.Failure("Error creating admin");
            }
        }

        public async Task<Result<AdminResponseDto>> GetAdminByIdAsync(Guid adminId)
        {
            try
            {
                var admin = await _adminRepository.GetByIdAsync(adminId);
                if (admin == null)
                    return Result<AdminResponseDto>.NotFound("Admin not found");

                return Result<AdminResponseDto>.Success(AdminMapper.ToResponseDto(admin));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting admin: {AdminId}", adminId);
                return Result<AdminResponseDto>.Failure("Error retrieving admin");
            }
        }

        public async Task<Result<IEnumerable<AdminResponseDto>>> GetAllAdminsAsync()
        {
            try
            {
                var admins = await _adminRepository.GetAllAsync();
                return Result<IEnumerable<AdminResponseDto>>.Success(admins.Select(AdminMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all admins");
                return Result<IEnumerable<AdminResponseDto>>.Failure("Error retrieving admins");
            }
        }

        public async Task<Result<AdminResponseDto>> UpdateAdminAsync(Guid adminId, AdminUpdateDto dto)
        {
            try
            {
                var admin = await _adminRepository.GetByIdAsync(adminId);
                if (admin == null)
                    return Result<AdminResponseDto>.NotFound("Admin not found");

                if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != admin.Email)
                {
                    var emailUnique = await _adminRepository.IsEmailUniqueAsync(dto.Email, adminId);
                    if (!emailUnique)
                        return Result<AdminResponseDto>.Failure("Email already exists");
                }
                AdminMapper.UpdateFromDto(admin, dto);
                await _adminRepository.UpdateAsync(admin);
                await _unitOfWork.CompleteAsync();

                return Result<AdminResponseDto>.Success(AdminMapper.ToResponseDto(admin));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating admin: {AdminId}", adminId);
                return Result<AdminResponseDto>.Failure("Error updating admin");
            }
        }

        public async Task<Result<bool>> DeleteAdminAsync(Guid adminId)
        {
            try
            {

                var admin = await _adminRepository.GetByIdAsync(adminId);
                if (admin == null)
                    return Result<bool>.NotFound("Admin not found");

                await _adminRepository.DeleteAsync(admin);
                await _unitOfWork.CompleteAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting admin: {AdminId}", adminId);
                return Result<bool>.Failure("Error deleting admin");
            }
        }

        public async Task<Result<AdminResponseDto>> ChangePasswordAsync(Guid adminId, AdminChangePasswordDto dto)
        {
            try
            {
                var admin = await _adminRepository.GetByIdAsync(adminId);
                if (admin == null)
                    return Result<AdminResponseDto>.NotFound("Admin not found");

                if (!_passwordHasher.VerifyPassword(dto.CurrentPassword, admin.PasswordHash))
                    return Result<AdminResponseDto>.Failure("Current password is incorrect");

                admin.PasswordHash = _passwordHasher.HashPassword(dto.NewPassword);
                admin.LastPasswordChange = DateTime.UtcNow;
                admin.UpdatedAt = DateTime.UtcNow;

                await _adminRepository.UpdateAsync(admin);
                await _unitOfWork.CompleteAsync();

                return Result<AdminResponseDto>.Success(AdminMapper.ToResponseDto(admin));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing admin password: {AdminId}", adminId);
                return Result<AdminResponseDto>.Failure("Error changing password");
            }
        }
    }
}