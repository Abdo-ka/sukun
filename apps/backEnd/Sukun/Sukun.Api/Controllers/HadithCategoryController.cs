using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Application.Interfaces;

[ApiController]
[Route("api/hadith-[controller]s")] // ينتج: /api/hadith-categories
public class HadithCategoryController : ControllerBase
{
    private readonly IHadithCategoryService _categoryService;

    public HadithCategoryController(IHadithCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/hadith-categories
    [HttpGet]
    public async Task<ApiResult<IEnumerable<HadithCategoryResponseDto>>> GetAll()
        => this.ToApiResult(await _categoryService.GetAllAsync());

    // GET: api/hadith-categories/{id:guid}
    [HttpGet("{id:guid}")]
    public async Task<ApiResult<HadithCategoryResponseDto>> GetById(Guid id)
        => this.ToApiResult(await _categoryService.GetByIdAsync(id));

    // GET: api/hadith-categories/{id:guid}/count
    [HttpGet("{id:guid}/count")]
    public async Task<ApiResult<int>> GetHadithsCount(Guid id)
        => this.ToApiResult(await _categoryService.GetHadithsCountAsync(id));

    // POST: api/hadith-categories
    [HttpPost]
    public async Task<ApiResult<HadithCategoryResponseDto>> Create([FromBody] HadithCategoryCreateDto dto)
        => this.ToApiResult(await _categoryService.CreateAsync(dto));

    // PUT: api/hadith-categories/{id:guid}
    [HttpPut("{id:guid}")]
    public async Task<ApiResult<HadithCategoryResponseDto>> Update(Guid id, [FromBody] HadithCategoryUpdateDto dto)
        => this.ToApiResult(await _categoryService.UpdateAsync(id, dto));

    // DELETE: api/hadith-categories/{id:guid} (Soft Delete)
    [HttpDelete("{id:guid}")]
    public async Task<ApiResult> SoftDelete(Guid id)
        => this.ToApiResult(await _categoryService.SoftDeleteAsync(id));
}