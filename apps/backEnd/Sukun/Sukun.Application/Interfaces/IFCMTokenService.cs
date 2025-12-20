using Sukun.Application.Dtos.FCMToken.Request;
using Sukun.Application.Dtos.FCMToken.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IFCMTokenService
    {
        Task<Result<FCMTokenResponseDto>> GetByIdAsync(Guid tokenId);
        Task<Result<FCMTokenResponseDto>> GetByTokenAsync(string token);
        Task<Result<IEnumerable<FCMTokenResponseDto>>> GetTokensByDeviceAsync(Guid deviceId);
        Task<Result<IEnumerable<FCMTokenResponseDto>>> GetActiveTokensByDeviceAsync(Guid deviceId);
        Task<Result<IEnumerable<FCMTokenResponseDto>>> GetActiveTokensByUserAsync(Guid userId);
        Task<Result<FCMTokenResponseDto>> UpdateLastUsedAsync(Guid tokenId);
        Task<Result<FCMTokenResponseDto>> DeactivateTokenAsync(Guid tokenId);
        Task<Result<FCMTokenResponseDto>> ActivateTokenAsync(Guid tokenId);
        Task<Result<int>> DeactivateAllTokensForDeviceAsync(Guid deviceId);
        Task<Result<bool>> TokenExistsAsync(string token);
        Task<Result<int>> CountActiveTokensByUserAsync(Guid userId);
        Task<Result<IEnumerable<FCMTokenResponseDto>>> GetExpiredTokensAsync(DateTime cutoffDate);
        Task<Result<int>> CleanupExpiredTokensAsync(DateTime cutoffDate);
        Task<Result<FCMTokenResponseDto>> CreateTokenAsync(FCMTokenCreateDto dto);
        Task<Result<FCMTokenResponseDto>> UpdateTokenAsync(Guid tokenId, FCMTokenUpdateDto dto);
        Task<Result<bool>> DeleteTokenAsync(Guid tokenId);
    }
}