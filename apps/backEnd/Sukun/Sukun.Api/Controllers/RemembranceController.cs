using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Seeder.Remembrance_entity;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/remembrances")]
    public class RemembranceController : ControllerBase
    {
        private readonly IRemembranceService _remembranceService;
        private readonly IRemembranceSeederService _remembranceSeederService;
        public RemembranceController(IRemembranceService remembranceService, IRemembranceSeederService remembranceSeederService)
        {
            _remembranceService = remembranceService;
            _remembranceSeederService = remembranceSeederService;
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<ApiResult<IEnumerable<RemembranceResponseDto>>> GetByCategory(Guid categoryId)
            => this.ToApiResult(await _remembranceService.GetByCategoryAsync(categoryId));

        [HttpGet("{id:guid}")]
        public async Task<ApiResult<RemembranceResponseDto>> GetById(Guid id)
            => this.ToApiResult(await _remembranceService.GetByIdAsync(id));
        
        [HttpGet("random")]
        public async Task<ApiResult<RemembranceResponseDto>> GetRandom()
            => this.ToApiResult(await _remembranceService.GetRandomAsync());


        [HttpPost]
        public async Task<ApiResult<RemembranceResponseDto>> Create([FromBody] RemembranceCreateDto dto)
            => this.ToApiResult(await _remembranceService.CreateAsync(dto));

        [HttpPut("{id:guid}")]
        public async Task<ApiResult<RemembranceResponseDto>> Update(Guid id, [FromBody] RemembranceUpdateDto dto)
            => this.ToApiResult(await _remembranceService.UpdateAsync(id, dto));

        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _remembranceService.SoftDeleteAsync(id));
        
        [HttpPost("seed")]
        // [Authorize(Roles = "Admin")] 
        public async Task<ApiResult<string>> SeedRemembrances()
        {
            try
            {
                await _remembranceSeederService.SeedRemembrancesAsync();
                return ApiResult<string>.Ok("تم إضافة الأذكار بنجاح من حصن المسلم والأذكار اليومية المشهورة.");
            }
            catch (Exception ex)
            {
                return ApiResult<string>.InternalServerError($"فشل تشغيل الـ Seed: {ex.Message}");
            }
        }
    }
}
