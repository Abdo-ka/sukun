using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Seeder.Hadith_entity;

[ApiController]
[Route("api/[controller]s")] // ينتج: /api/hadiths
public class HadithController : ControllerBase
{
    private readonly IHadithService _hadithService;
    private readonly IHadithSeederService _hadithSeederService;

    public HadithController(IHadithService hadithService ,IHadithSeederService hadithSeederService)
    {
        _hadithService = hadithService;
        _hadithSeederService = hadithSeederService;
    }

    #region Public Endpoints (للتطبيق والمستخدمين)

    // GET: api/hadiths/categories
    [HttpGet("categories")]
    public async Task<ApiResult<IEnumerable<HadithCategoryResponseDto>>> GetCategories()
        => this.ToApiResult(await _hadithService.GetAllCategoriesAsync());

    // GET: api/hadiths/paged?pageNumber=1&pageSize=20&searchTerm=صدقة
    [HttpGet("paged")]
    public async Task<ApiResult<PagedResponseDto<HadithListResponseDto>>> GetPaged([FromQuery] PagedRequestDto request)
        => this.ToApiResult(await _hadithService.GetPagedAsync(request));

    // GET: api/hadiths/category/{categoryId:guid}
    [HttpGet("category/{categoryId:guid}")]
    public async Task<ApiResult<IEnumerable<HadithListResponseDto>>> GetByCategory(Guid categoryId)
        => this.ToApiResult(await _hadithService.GetByCategoryAsync(categoryId));

    // GET: api/hadiths/{id:guid}
    [HttpGet("{id:guid}")]
    public async Task<ApiResult<HadithResponseDto>> GetById(Guid id)
        => this.ToApiResult(await _hadithService.GetByIdAsync(id));

    // GET: api/hadiths/random?count=5
    [HttpGet("random")]
    public async Task<ApiResult<IEnumerable<HadithListResponseDto>>> GetRandom([FromQuery] int count = 5)
        => this.ToApiResult(await _hadithService.GetRandomAsync(count));

    // GET: api/hadiths/search?query=الصلاة&pageNumber=1&pageSize=20
    [HttpGet("search")]
    public async Task<ApiResult<PagedResponseDto<HadithListResponseDto>>> Search(
        [FromQuery] string query,
        [FromQuery] PagedRequestDto? paging = null)
        => this.ToApiResult(await _hadithService.SearchAsync(query, paging));

    #endregion

    #region Admin CRUD (يمكن إضافة [Authorize(Roles = "Admin")] لاحقًا)

    // POST: api/hadiths
    [HttpPost]
    public async Task<ApiResult<HadithResponseDto>> Create([FromBody] HadithCreateDto dto)
        => this.ToApiResult(await _hadithService.CreateAsync(dto));

    // PUT: api/hadiths/{id:guid}
    [HttpPut("{id:guid}")]
    public async Task<ApiResult<HadithResponseDto>> Update(Guid id, [FromBody] HadithUpdateDto dto)
        => this.ToApiResult(await _hadithService.UpdateAsync(id, dto));

    // DELETE: api/hadiths/{id:guid} (Soft Delete)
    [HttpDelete("{id:guid}")]
    public async Task<ApiResult> SoftDelete(Guid id)
        => this.ToApiResult(await _hadithService.SoftDeleteAsync(id));
    [HttpPost("seed")]
    [Authorize(Roles = "SuperAdmin")] 
    public async Task<ApiResult<string>> SeedHadiths()
    {
        try
        {
            await _hadithSeederService.SeedHadithsAsync();
            return ApiResult<string>.Ok("Hadiths seeded successfully from external source.");
        }
        catch (Exception ex)
        {
            // يمكنك إضافة logging هنا
            return ApiResult<string>.InternalServerError($"Seeding failed: {ex.Message}");
        }
    }
    #endregion
}
