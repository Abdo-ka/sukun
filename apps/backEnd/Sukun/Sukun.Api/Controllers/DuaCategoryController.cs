using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Dua.Response;
using Sukun.Application.Dtos.DuaCategory.Request;
using Sukun.Application.Dtos.DuaCategory.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/dua-categories")]
    public class DuaCategoryController : ControllerBase
    {
        private readonly IDuaCategoryService _categoryService;

        public DuaCategoryController(IDuaCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/dua-categories
        [HttpGet]
        public async Task<ApiResult<IEnumerable<DuaCategoryResponseDto>>> GetAll()
            => this.ToApiResult(await _categoryService.GetAllAsync());

        // GET: api/dua-categories/{id:guid}
        [HttpGet("{id:guid}")]
        public async Task<ApiResult<DuaCategoryWithDuasDto>> GetById(Guid id)
            => this.ToApiResult(await _categoryService.GetByIdWithDuasAsync(id));

        // ====================== Admin Operations ======================

        // POST: api/dua-categories
        [HttpPost]
        public async Task<ApiResult<DuaCategoryResponseDto>> Create([FromBody] DuaCategoryCreateDto dto)
            => this.ToApiResult(await _categoryService.CreateAsync(dto));

        // PUT: api/dua-categories/{id:guid}
        [HttpPut("{id:guid}")]
        public async Task<ApiResult<DuaCategoryResponseDto>> Update(Guid id, [FromBody] DuaCategoryUpdateDto dto)
            => this.ToApiResult(await _categoryService.UpdateAsync(id, dto));

        // DELETE: api/dua-categories/{id:guid} (Soft Delete)
        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _categoryService.SoftDeleteAsync(id));
    }
}
