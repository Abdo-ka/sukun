using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class FCMTokenRepository : Repository<FCMToken>, IFCMTokenRepository
    {
        public FCMTokenRepository(ApplicationDbContext context, ILogger<FCMTokenRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<FCMToken?> GetByTokenAsync(string token)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(t => t.Token == token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting FCM token by token string: {Token}", token);
                throw;
            }
        }

        public async Task<IEnumerable<FCMToken>> GetTokensByDeviceAsync(Guid deviceId)
        {
            try
            {
                return await _dbSet
                    .Where(t => t.UserDeviceId == deviceId)
                    .OrderByDescending(t => t.LastUsedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tokens by device: {DeviceId}", deviceId);
                throw;
            }
        }

        public async Task<IEnumerable<FCMToken>> GetActiveTokensByDeviceAsync(Guid deviceId)
        {
            try
            {
                return await _dbSet
                    .Where(t => t.UserDeviceId == deviceId && t.IsActive)
                    .OrderByDescending(t => t.LastUsedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active tokens by device: {DeviceId}", deviceId);
                throw;
            }
        }

        public async Task<IEnumerable<FCMToken>> GetActiveTokensByUserAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Include(t => t.Device)
                    .Where(t => t.Device!.UserId == userId && t.IsActive)
                    .OrderByDescending(t => t.LastUsedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active tokens by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<Result<FCMToken>> UpdateLastUsedAsync(Guid tokenId)
        {
            try
            {
                var token = await GetByIdAsync(tokenId);
                if (token == null)
                    return Result<FCMToken>.Failure($"Token with ID {tokenId} not found");

                token.LastUsedAt = DateTime.UtcNow;
                token.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(token);
                await _context.SaveChangesAsync();

                return Result<FCMToken>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last used for token: {TokenId}", tokenId);
                return Result<FCMToken>.Failure(ex.Message);
            }
        }

        public async Task<Result<FCMToken>> DeactivateTokenAsync(Guid tokenId)
        {
            try
            {
                var token = await GetByIdAsync(tokenId);
                if (token == null)
                    return Result<FCMToken>.Failure($"Token with ID {tokenId} not found");

                token.IsActive = false;
                token.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(token);
                await _context.SaveChangesAsync();

                return Result<FCMToken>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating token: {TokenId}", tokenId);
                return Result<FCMToken>.Failure(ex.Message);
            }
        }

        public async Task<Result<FCMToken>> ActivateTokenAsync(Guid tokenId)
        {
            try
            {
                var token = await GetByIdAsync(tokenId);
                if (token == null)
                    return Result<FCMToken>.Failure($"Token with ID {tokenId} not found");

                token.IsActive = true;
                token.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(token);
                await _context.SaveChangesAsync();

                return Result<FCMToken>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating token: {TokenId}", tokenId);
                return Result<FCMToken>.Failure(ex.Message);
            }
        }

        public async Task<Result<int>> DeactivateAllTokensForDeviceAsync(Guid deviceId)
        {
            try
            {
                var tokens = await GetTokensByDeviceAsync(deviceId);

                if (!tokens.Any())
                    return Result<int>.Success(0);

                foreach (var token in tokens)
                {
                    token.IsActive = false;
                    token.UpdatedAt = DateTime.UtcNow;
                }

                _dbSet.UpdateRange(tokens);
                var affectedRows = await _context.SaveChangesAsync();

                return Result<int>.Success(affectedRows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating all tokens for device: {DeviceId}", deviceId);
                return Result<int>.Failure(ex.Message);
            }
        }

        public async Task<bool> TokenExistsAsync(string token)
        {
            try
            {
                return await _dbSet
                    .AnyAsync(t => t.Token == token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking token existence: {Token}", token);
                throw;
            }
        }

        public async Task<int> CountActiveTokensByUserAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Include(t => t.Device)
                    .Where(t => t.Device!.UserId == userId && t.IsActive)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting active tokens by user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<FCMToken>> GetExpiredTokensAsync(DateTime cutoffDate)
        {
            try
            {
                return await _dbSet
                    .Where(t => t.LastUsedAt < cutoffDate && t.IsActive)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting expired tokens before: {CutoffDate}", cutoffDate);
                throw;
            }
        }

        public async Task<Result<int>> CleanupExpiredTokensAsync(DateTime cutoffDate)
        {
            try
            {
                var expiredTokens = await GetExpiredTokensAsync(cutoffDate);

                if (!expiredTokens.Any())
                    return Result<int>.Success(0);

                foreach (var token in expiredTokens)
                {
                    token.IsActive = false;
                    token.UpdatedAt = DateTime.UtcNow;
                }

                _dbSet.UpdateRange(expiredTokens);
                var affectedRows = await _context.SaveChangesAsync();

                return Result<int>.Success(affectedRows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up expired tokens before: {CutoffDate}", cutoffDate);
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
