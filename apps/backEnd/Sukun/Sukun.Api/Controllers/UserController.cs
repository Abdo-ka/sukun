using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.User.Responce;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

        [HttpPost]
        public async Task<ApiResult<UserResponseDto>> Create(Guid userId)
            => this.ToApiResult(await _userService.GetOrCreateAnonymousUserAsync(userId));

        [HttpGet("exist")]
        public async Task<ApiResult<bool>> UserExistsAsync(Guid userId)
            => this.ToApiResult(await _userService.UserExistsAsync(userId));

        [HttpGet("{userId:guid}/detail")]
        public async Task<ApiResult<UserDetailsResponseDto>> GetDetail(Guid userId)
            => this.ToApiResult(await _userService.GetUserDetailAsync(userId));

        [HttpPut("{userId:guid}")]
        public async Task<ApiResult> Update(Guid userId, [FromBody] Guid cityId)
            => this.ToApiResult(await _userService.UpdateUserCityAsync(userId, cityId));

        //[HttpGet]
        //public async Task<ApiResult<PagedResponseDto<UserResponseDto>>> GetAll([FromQuery] PagedRequestDto request)
        //    => this.ToApiResult(await _userService.GetUsersAsync(request));

        ////[HttpGet("search")]
        ////public async Task<ApiResult<PagedResponseDto<UserResponseDto>>> Search([FromQuery] SearchRequestDto request)
        ////    => this.ToApiResult(await _userService.SearchUsersAsync(request));


        //[HttpDelete("{userId:guid}")]
        //public async Task<ApiResult<bool>> Delete(Guid userId)
        //    => this.ToApiResult(await _userService.DeleteUserAsync(userId));

        //[HttpPatch("{userId:guid}/deactivate")]
        //public async Task<ApiResult<UserResponseDto>> Deactivate(Guid userId)
        //    => this.ToApiResult(await _userService.DeactivateUserAsync(userId));

        //[HttpPatch("{userId:guid}/activate")]
        //public async Task<ApiResult<UserResponseDto>> Activate(Guid userId)
        //    => this.ToApiResult(await _userService.ActivateUserAsync(userId));

        //[HttpPatch("{userId:guid}/change-password")]
        //public async Task<ApiResult<UserResponseDto>> ChangePassword(Guid userId, [FromBody] UserChangePasswordDto request)
        //    => this.ToApiResult(await _userService.ChangePasswordAsync(userId, request));

        //[HttpPatch("{userId:guid}/last-login")]
        //public async Task<ApiResult<UserResponseDto>> UpdateLastLogin(Guid userId/*, [FromBody] UpdateLastLoginRequest? request = null*/)
        //    => this.ToApiResult(await _userService.UpdateLastLoginAsync(userId/*, request?.DeviceInfo*/));

        //[HttpGet("stats")]
        //public async Task<ApiResult<Dictionary<UserRole, int>>> GetStats()
        //    => this.ToApiResult(await _userService.GetUserStatsAsync());
    }
 
}
