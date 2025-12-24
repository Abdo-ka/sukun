using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.RemembranceCategory.Request;
using Sukun.Application.Dtos.RemembranceCategory.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/remembrance-categories")]
    public class RemembranceCategoryController : ControllerBase
    {
        private readonly IRemembranceCategoryService _categoryService;

        public RemembranceCategoryController(IRemembranceCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<RemembranceCategoryResponseDto>>> GetAll()
            => this.ToApiResult(await _categoryService.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<ApiResult<RemembranceCategoryWithRemembrancesDto>> GetById(Guid id)
            => this.ToApiResult(await _categoryService.GetByIdWithRemembrancesAsync(id));


        [HttpPost]
        public async Task<ApiResult<RemembranceCategoryResponseDto>> Create([FromBody] RemembranceCategoryCreateDto dto)
            => this.ToApiResult(await _categoryService.CreateAsync(dto));

        [HttpPut("{id:guid}")]
        public async Task<ApiResult<RemembranceCategoryResponseDto>> Update(Guid id, [FromBody] RemembranceCategoryUpdateDto dto)
            => this.ToApiResult(await _categoryService.UpdateAsync(id, dto));

        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _categoryService.SoftDeleteAsync(id));
    }
}
