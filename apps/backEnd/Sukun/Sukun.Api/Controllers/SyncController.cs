using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/sync")]
    public class SyncController : ControllerBase
    {
        private readonly IDataVersionService _dataVersionService;

        public SyncController(IDataVersionService dataVersionService)
        {
            _dataVersionService = dataVersionService;
        }

        // GET: api/sync/versions
        [HttpGet("versions")]
        public async Task<ApiResult<Dictionary<string, long>>> GetVersions()
        {
            var versions = await _dataVersionService.GetAllVersionsAsync();
            return ApiResult<Dictionary<string, long>>.Ok(versions);
        }
    }
}
