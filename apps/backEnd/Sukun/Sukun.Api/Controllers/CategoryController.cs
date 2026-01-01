using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.NarrativeCategory.Request;
using Sukun.Application.Dtos.NarrativeCategory.Response;
using Sukun.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class NarrativeCategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public NarrativeCategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ApiResult<IEnumerable<CategoryResponseDto>>> GetAllAsync()
     => this.ToApiResult(await _categoryService.GetAllAsync());

    [HttpGet("root")]
    public async Task<ApiResult<IEnumerable<CategoryResponseDto>>> GetRoot()
     => this.ToApiResult(await _categoryService.GetRootAsync());
   
    [HttpGet("main")]
    public async Task<ApiResult<IEnumerable<CategoryResponseDto>>> GetMainSections()
    => this.ToApiResult(await _categoryService.GetMainSectionsAsync());

    [HttpGet("{id:guid}/tree")]
    public async Task<ApiResult<CategoryWithChildrenResponseDto>> GetTree(Guid id)
     => this.ToApiResult(await _categoryService.GetByIdWithChildrenAsync(id));

    [HttpGet("{id:guid}")]
    public async Task<ApiResult<CategoryWithNarrativesResponseDto>> GetWithNarratives(
        Guid id)
      => this.ToApiResult(await _categoryService.GetByIdWithNarrativesAsync(id));

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResult<CategoryResponseDto>> Create([FromBody] CategoryCreateDto dto)
     => this.ToApiResult(await _categoryService.CreateAsync(dto));

    [HttpPut("{id:guid}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResult<CategoryResponseDto>> Update(Guid id, [FromBody] CategoryUpdateDto dto)
      => this.ToApiResult(await _categoryService.UpdateAsync(id, dto));

    [HttpDelete("{id:guid}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResult<bool>> SoftDelete(Guid id)
     => this.ToApiResult(await _categoryService.SoftDeleteAsync(id));
}

