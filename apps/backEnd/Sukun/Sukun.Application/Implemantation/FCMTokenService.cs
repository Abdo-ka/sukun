using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.FCMToken.Request;
using Sukun.Application.Dtos.FCMToken.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{

    public class FCMTokenService : BaseService, IFCMTokenService
    {


        public FCMTokenService(IUnitOfWork unitOfWork, ILogger<FCMTokenService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Result<FCMTokenResponseDto>> GetByIdAsync(Guid tokenId)
        {
            try
            {
                _logger.LogDebug("Getting FCM token by ID: {TokenId}", tokenId);
                var token = await _unitOfWork.FCMTokens.GetByIdAsync(tokenId);
                if (token == null)
                    return Result<FCMTokenResponseDto>.NotFound($"Token with ID {tokenId} not found");

                return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting FCM token by ID: {TokenId}", tokenId);
                return Result<FCMTokenResponseDto>.Failure($"Error retrieving token: {ex.Message}");
            }
        }

        public async Task<Result<FCMTokenResponseDto>> GetByTokenAsync(string token)
        {
            try
            {
                _logger.LogDebug("Getting FCM token by token string: {Token}", token);
                var fcmToken = await _unitOfWork.FCMTokens.GetByTokenAsync(token);
                if (fcmToken == null)
                    return Result<FCMTokenResponseDto>.NotFound($"Token {token} not found");

                return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(fcmToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting FCM token by token string");
                return Result<FCMTokenResponseDto>.Failure($"Error retrieving token: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<FCMTokenResponseDto>>> GetTokensByDeviceAsync(Guid deviceId)
        {
            try
            {
                _logger.LogDebug("Getting tokens by device: {DeviceId}", deviceId);
                var tokens = await _unitOfWork.FCMTokens.GetTokensByDeviceAsync(deviceId);
                return Result<IEnumerable<FCMTokenResponseDto>>.Success(tokens.Select(FCMTokenMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tokens by device: {DeviceId}", deviceId);
                return Result<IEnumerable<FCMTokenResponseDto>>.Failure($"Error retrieving tokens: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<FCMTokenResponseDto>>> GetActiveTokensByDeviceAsync(Guid deviceId)
        {
            try
            {
                _logger.LogDebug("Getting active tokens by device: {DeviceId}", deviceId);
                var tokens = await _unitOfWork.FCMTokens.GetActiveTokensByDeviceAsync(deviceId);
                return Result<IEnumerable<FCMTokenResponseDto>>.Success(tokens.Select(FCMTokenMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active tokens by device: {DeviceId}", deviceId);
                return Result<IEnumerable<FCMTokenResponseDto>>.Failure($"Error retrieving tokens: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<FCMTokenResponseDto>>> GetActiveTokensByUserAsync(Guid userId)
        {
            try
            {
                _logger.LogDebug("Getting active tokens by user: {UserId}", userId);
                var tokens = await _unitOfWork.FCMTokens.GetActiveTokensByUserAsync(userId);
                return Result<IEnumerable<FCMTokenResponseDto>>.Success(tokens.Select(FCMTokenMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active tokens by user: {UserId}", userId);
                return Result<IEnumerable<FCMTokenResponseDto>>.Failure($"Error retrieving tokens: {ex.Message}");
            }
        }

        public async Task<Result<FCMTokenResponseDto>> UpdateLastUsedAsync(Guid tokenId)
        {
            try
            {
                _logger.LogDebug("Updating last used for token: {TokenId}", tokenId);
                var result = await _unitOfWork.FCMTokens.UpdateLastUsedAsync(tokenId);
                if (!result.IsSuccess)
                    return Result<FCMTokenResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last used for token: {TokenId}", tokenId);
                return Result<FCMTokenResponseDto>.Failure($"Error updating token: {ex.Message}");
            }
        }

        public async Task<Result<FCMTokenResponseDto>> DeactivateTokenAsync(Guid tokenId)
        {
            try
            {
                _logger.LogDebug("Deactivating token: {TokenId}", tokenId);
                var result = await _unitOfWork.FCMTokens.DeactivateTokenAsync(tokenId);
                if (!result.IsSuccess)
                    return Result<FCMTokenResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating token: {TokenId}", tokenId);
                return Result<FCMTokenResponseDto>.Failure($"Error updating token: {ex.Message}");
            }
        }

        public async Task<Result<FCMTokenResponseDto>> ActivateTokenAsync(Guid tokenId)
        {
            try
            {
                _logger.LogDebug("Activating token: {TokenId}", tokenId);
                var result = await _unitOfWork.FCMTokens.ActivateTokenAsync(tokenId);
                if (!result.IsSuccess)
                    return Result<FCMTokenResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating token: {TokenId}", tokenId);
                return Result<FCMTokenResponseDto>.Failure($"Error updating token: {ex.Message}");
            }
        }

        public async Task<Result<int>> DeactivateAllTokensForDeviceAsync(Guid deviceId)
        {
            try
            {
                _logger.LogDebug("Deactivating all tokens for device: {DeviceId}", deviceId);
                var result = await _unitOfWork.FCMTokens.DeactivateAllTokensForDeviceAsync(deviceId);
                await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating all tokens for device: {DeviceId}", deviceId);
                return Result<int>.Failure($"Error updating tokens: {ex.Message}");
            }
        }

        public async Task<Result<bool>> TokenExistsAsync(string token)
        {
            try
            {
                _logger.LogDebug("Checking token existence: {Token}", token);
                var exists = await _unitOfWork.FCMTokens.TokenExistsAsync(token);
                return Result<bool>.Success(exists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking token existence");
                return Result<bool>.Failure($"Error checking token: {ex.Message}");
            }
        }

        public async Task<Result<int>> CountActiveTokensByUserAsync(Guid userId)
        {
            try
            {
                _logger.LogDebug("Counting active tokens by user: {UserId}", userId);
                var count = await _unitOfWork.FCMTokens.CountActiveTokensByUserAsync(userId);
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting active tokens by user: {UserId}", userId);
                return Result<int>.Failure($"Error counting tokens: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<FCMTokenResponseDto>>> GetExpiredTokensAsync(DateTime cutoffDate)
        {
            try
            {
                _logger.LogDebug("Getting expired tokens before: {CutoffDate}", cutoffDate);
                var tokens = await _unitOfWork.FCMTokens.GetExpiredTokensAsync(cutoffDate);
                return Result<IEnumerable<FCMTokenResponseDto>>.Success(tokens.Select(FCMTokenMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting expired tokens");
                return Result<IEnumerable<FCMTokenResponseDto>>.Failure($"Error retrieving tokens: {ex.Message}");
            }
        }

        public async Task<Result<int>> CleanupExpiredTokensAsync(DateTime cutoffDate)
        {
            try
            {
                _logger.LogDebug("Cleaning up expired tokens before: {CutoffDate}", cutoffDate);
                var result = await _unitOfWork.FCMTokens.CleanupExpiredTokensAsync(cutoffDate);
                await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up expired tokens");
                return Result<int>.Failure($"Error cleaning up tokens: {ex.Message}");
            }
        }
        public async Task<Result<FCMTokenResponseDto>> CreateTokenAsync(FCMTokenCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Creating new FCM token for device: {DeviceId}", dto.UserDeviceId);
                var existingToken = await _unitOfWork.FCMTokens.GetByTokenAsync(dto.Token);
                if (existingToken != null)
                {
                    existingToken.IsActive = true;
                    existingToken.LastUsedAt = DateTime.UtcNow;
                    existingToken.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.FCMTokens.UpdateAsync(existingToken);
                    await _unitOfWork.CompleteAsync();
                    return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(existingToken));
                }

                var token = FCMTokenMapper.FromCreateDto(dto);
                await _unitOfWork.FCMTokens.AddAsync(token);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("FCM token created successfully with ID: {TokenId}", token.Id);
                return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating FCM token for device: {DeviceId}", dto.UserDeviceId);
                return Result<FCMTokenResponseDto>.Failure($"Error creating FCM token: {ex.Message}");
            }
        }
        public async Task<Result<FCMTokenResponseDto>> UpdateTokenAsync(Guid tokenId, FCMTokenUpdateDto dto)
        {
            try
            {
                _logger.LogInformation("Updating FCM token with ID: {TokenId}", tokenId);
                var token = await _unitOfWork.FCMTokens.GetByIdAsync(tokenId);
                if (token == null)
                    return Result<FCMTokenResponseDto>.NotFound($"FCM token with ID {tokenId} not found");

                if (dto.IsActive.HasValue)
                    token.IsActive = dto.IsActive.Value;
                token.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.FCMTokens.UpdateAsync(token);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("FCM token updated successfully with ID: {TokenId}", tokenId);
                return Result<FCMTokenResponseDto>.Success(FCMTokenMapper.ToResponseDto(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating FCM token with ID: {TokenId}", tokenId);
                return Result<FCMTokenResponseDto>.Failure($"Error updating FCM token: {ex.Message}");
            }
        }
        public async Task<Result<bool>> DeleteTokenAsync(Guid tokenId)
        {
            try
            {
                _logger.LogInformation("Deleting FCM token with ID: {TokenId}", tokenId);
                var token = await _unitOfWork.FCMTokens.GetByIdAsync(tokenId);
                if (token == null)
                    return Result<bool>.NotFound($"FCM token with ID {tokenId} not found");

                await _unitOfWork.FCMTokens.DeleteAsync(token);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("FCM token deleted successfully with ID: {TokenId}", tokenId);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting FCM token with ID: {TokenId}", tokenId);
                return Result<bool>.Failure($"Error deleting FCM token: {ex.Message}");
            }
        }
    }
}