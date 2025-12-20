using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.UserDevice.Request;
using Sukun.Application.Dtos.UserDevice.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class UserDeviceController : ControllerBase
    {
        private readonly IUserDeviceService _deviceService;

        public UserDeviceController(IUserDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost]
        public async Task<ApiResult<UserDeviceResponseDto>> Create(Guid userId ,[FromBody] UserDeviceCreateDto deviceDto)
            => this.ToApiResult(await _deviceService.CreateDeviceAsync(userId, deviceDto));

        [HttpGet]
        public async Task<ApiResult<IEnumerable<UserDeviceResponseDto>>> GetAll([FromQuery] Guid userId)
            => this.ToApiResult(await _deviceService.GetDevicesByUserAsync(userId));

        [HttpGet("{deviceId:guid}")]
        public async Task<ApiResult<UserDeviceResponseDto>> GetById(Guid deviceId)
            => this.ToApiResult(await _deviceService.GetByIdAsync(deviceId));

        [HttpPut("{deviceId:guid}")]
        public async Task<ApiResult<UserDeviceResponseDto>> Update(Guid deviceId, [FromBody] UserDeviceUpdateDto request)
            => this.ToApiResult(await _deviceService.UpdateDeviceAsync(deviceId, request));

        [HttpDelete("{deviceId:guid}")]
        public async Task<ApiResult<bool>> Delete(Guid deviceId)
            => this.ToApiResult(await _deviceService.DeleteDeviceAsync(deviceId));

        [HttpPatch("{deviceId:guid}/last-active")]
        public async Task<ApiResult<UserDeviceResponseDto>> UpdateLastActive(Guid deviceId)
            => this.ToApiResult(await _deviceService.UpdateLastActiveAsync(deviceId));

        [HttpPatch("{deviceId:guid}/enable-notifications")]
        public async Task<ApiResult<UserDeviceResponseDto>> EnableNotifications(Guid deviceId)
            => this.ToApiResult(await _deviceService.EnableNotificationsAsync(deviceId));

        [HttpPatch("{deviceId:guid}/disable-notifications")]
        public async Task<ApiResult<UserDeviceResponseDto>> DisableNotifications(Guid deviceId)
            => this.ToApiResult(await _deviceService.DisableNotificationsAsync(deviceId));
    }


}
