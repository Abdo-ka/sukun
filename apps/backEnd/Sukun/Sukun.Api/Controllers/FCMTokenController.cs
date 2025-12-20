using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.FCMToken.Request;
using Sukun.Application.Dtos.FCMToken.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/fcm-tokens")]
    public class FCMTokenController : ControllerBase
    {
        private readonly IFCMTokenService _tokenService;

        public FCMTokenController(IFCMTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ApiResult<FCMTokenResponseDto>> Create([FromBody] FCMTokenCreateDto request)
            => this.ToApiResult(await _tokenService.CreateTokenAsync(request));

        [HttpGet("{tokenId:guid}")]
        public async Task<ApiResult<FCMTokenResponseDto>> GetById(Guid tokenId)
            => this.ToApiResult(await _tokenService.GetByIdAsync(tokenId));

        [HttpGet("device/{deviceId:guid}")]
        public async Task<ApiResult<IEnumerable<FCMTokenResponseDto>>> GetByDevice(Guid deviceId)
            => this.ToApiResult(await _tokenService.GetTokensByDeviceAsync(deviceId));

        [HttpGet("user/{userId:guid}/active")]
        public async Task<ApiResult<IEnumerable<FCMTokenResponseDto>>> GetActiveByUser(Guid userId)
            => this.ToApiResult(await _tokenService.GetActiveTokensByUserAsync(userId));

        [HttpPut("{tokenId:guid}")]
        public async Task<ApiResult<FCMTokenResponseDto>> Update(Guid tokenId, [FromBody] FCMTokenUpdateDto request)
            => this.ToApiResult(await _tokenService.UpdateTokenAsync(tokenId, request));

        [HttpDelete("{tokenId:guid}")]
        public async Task<ApiResult<bool>> Delete(Guid tokenId)
            => this.ToApiResult(await _tokenService.DeleteTokenAsync(tokenId));

        [HttpPatch("{tokenId:guid}/last-used")]
        public async Task<ApiResult<FCMTokenResponseDto>> UpdateLastUsed(Guid tokenId)
            => this.ToApiResult(await _tokenService.UpdateLastUsedAsync(tokenId));

        [HttpPatch("{tokenId:guid}/deactivate")]
        public async Task<ApiResult<FCMTokenResponseDto>> Deactivate(Guid tokenId)
            => this.ToApiResult(await _tokenService.DeactivateTokenAsync(tokenId));

        [HttpPatch("{tokenId:guid}/activate")]
        public async Task<ApiResult<FCMTokenResponseDto>> Activate(Guid tokenId)
            => this.ToApiResult(await _tokenService.ActivateTokenAsync(tokenId));

        [HttpPost("device/{deviceId:guid}/deactivate-all")]
        public async Task<ApiResult<int>> DeactivateAllForDevice(Guid deviceId)
            => this.ToApiResult(await _tokenService.DeactivateAllTokensForDeviceAsync(deviceId));

        [HttpPost("cleanup")]
        public async Task<ApiResult<int>> CleanupExpired([FromQuery] DateTime cutoffDate)
            => this.ToApiResult(await _tokenService.CleanupExpiredTokensAsync(cutoffDate));
    }
 
}
