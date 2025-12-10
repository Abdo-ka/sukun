using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IFCMTokenRepository : IRepository<FCMToken>
    {
        Task<FCMToken?> GetByTokenAsync(string token);
        Task<IEnumerable<FCMToken>> GetTokensByDeviceAsync(Guid deviceId);
        Task<IEnumerable<FCMToken>> GetActiveTokensByDeviceAsync(Guid deviceId);
        Task<IEnumerable<FCMToken>> GetActiveTokensByUserAsync(Guid userId);
        Task<Result<FCMToken>> UpdateLastUsedAsync(Guid tokenId);
        Task<Result<FCMToken>> DeactivateTokenAsync(Guid tokenId);
        Task<Result<FCMToken>> ActivateTokenAsync(Guid tokenId);
        Task<Result<int>> DeactivateAllTokensForDeviceAsync(Guid deviceId);
        Task<bool> TokenExistsAsync(string token);
        Task<int> CountActiveTokensByUserAsync(Guid userId);
        Task<IEnumerable<FCMToken>> GetExpiredTokensAsync(DateTime cutoffDate);
        Task<Result<int>> CleanupExpiredTokensAsync(DateTime cutoffDate);
    }
}
