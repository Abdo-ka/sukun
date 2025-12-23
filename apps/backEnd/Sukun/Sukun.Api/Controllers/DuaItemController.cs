using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Dua.Request;
using Sukun.Application.Dtos.Dua.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Seeder.Dua_entity;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/dua-items")]
    public class DuaItemController : ControllerBase
    {
        private readonly IDuaItemService _duaItemService;

        public IDuaSeederService _duaSeederService { get; }

        public DuaItemController(IDuaItemService duaItemService , IDuaSeederService duaSeederService)
        {
            _duaItemService = duaItemService;
            _duaSeederService = duaSeederService;
        }

        // GET: api/dua-items/category/{categoryId:guid}
        [HttpGet("category/{categoryId:guid}")]
        public async Task<ApiResult<IEnumerable<DuaItemResponseDto>>> GetByCategory(Guid categoryId)
            => this.ToApiResult(await _duaItemService.GetByCategoryAsync(categoryId));

        // GET: api/dua-items/{id:guid}
        [HttpGet("{id:guid}")]
        public async Task<ApiResult<DuaItemResponseDto>> GetById(Guid id)
            => this.ToApiResult(await _duaItemService.GetByIdAsync(id));

        // GET: api/dua-items/random
        [HttpGet("random")]
        public async Task<ApiResult<DuaItemResponseDto>> GetRandom()
            => this.ToApiResult(await _duaItemService.GetRandomAsync());

        // ====================== Admin Operations ======================

        // POST: api/dua-items
        [HttpPost]
        public async Task<ApiResult<DuaItemResponseDto>> Create([FromBody] DuaItemCreateDto dto)
            => this.ToApiResult(await _duaItemService.CreateAsync(dto));

        // PUT: api/dua-items/{id:guid}
        [HttpPut("{id:guid}")]
        public async Task<ApiResult<DuaItemResponseDto>> Update(Guid id, [FromBody] DuaItemUpdateDto dto)
            => this.ToApiResult(await _duaItemService.UpdateAsync(id, dto));

        // DELETE: api/dua-items/{id:guid} (Soft Delete)
        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _duaItemService.SoftDeleteAsync(id));

        [HttpPost("seed-Dua")]
        public async Task<ApiResult<string>> RunSeed()
        {
            try
            {
                await _duaSeederService.SeedDuasAsync();
                return ApiResult<string>.Ok("Dua Data seeded successfully");
            }
            catch (Exception ex)
            {
                return ApiResult<string>.InternalServerError($"Failed to seed Dua data");
            }
        }
    }
}
