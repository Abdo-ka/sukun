using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Tasbih.Request;
using Sukun.Application.Dtos.Tasbih.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/tasbihs")]
    public class TasbihController : ControllerBase
    {
        private readonly ITasbihService _tasbihService;

        public TasbihController(ITasbihService tasbihService)
        {
            _tasbihService = tasbihService;
        }

        // GET: api/tasbihs
        [HttpGet]
        public async Task<ApiResult<IEnumerable<TasbihResponseDto>>> GetAll()
            => this.ToApiResult(await _tasbihService.GetAllAsync());

        // GET: api/tasbihs/{id:guid}
        [HttpGet("{id:guid}")]
        public async Task<ApiResult<TasbihResponseDto>> GetById(Guid id)
            => this.ToApiResult(await _tasbihService.GetByIdAsync(id));

        // GET: api/tasbihs/random
        [HttpGet("random")]
        public async Task<ApiResult<TasbihResponseDto>> GetRandom()
            => this.ToApiResult(await _tasbihService.GetRandomAsync());

        // ====================== Admin Operations ======================

        // POST: api/tasbihs
        [HttpPost]
        public async Task<ApiResult<TasbihResponseDto>> Create([FromBody] TasbihCreateDto dto)
            => this.ToApiResult(await _tasbihService.CreateAsync(dto));

        // PUT: api/tasbihs/{id:guid}
        [HttpPut("{id:guid}")]
        public async Task<ApiResult<TasbihResponseDto>> Update(Guid id, [FromBody] TasbihUpdateDto dto)
            => this.ToApiResult(await _tasbihService.UpdateAsync(id, dto));

        // DELETE: api/tasbihs/{id:guid} (Soft Delete)
        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _tasbihService.SoftDeleteAsync(id));
    }
}
