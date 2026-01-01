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
[Route("api/[controller]s")] 
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

    [HttpGet("categories")]
    public async Task<ApiResult<IEnumerable<HadithCategoryResponseDto>>> GetCategories()
        => this.ToApiResult(await _hadithService.GetAllCategoriesAsync());

    [HttpGet("paged")]
    public async Task<ApiResult<PagedResponseDto<HadithListResponseDto>>> GetPaged([FromQuery] PagedRequestDto request, Guid? bookId)
        => this.ToApiResult(await _hadithService.GetPagedAsync(request, bookId));

    [HttpGet("category/{categoryId:guid}")]
    public async Task<ApiResult<IEnumerable<HadithListResponseDto>>> GetByCategory(Guid categoryId)
        => this.ToApiResult(await _hadithService.GetByCategoryAsync(categoryId));

    [HttpGet("{id:guid}")]
    public async Task<ApiResult<HadithResponseDto>> GetById(Guid id)
        => this.ToApiResult(await _hadithService.GetByIdAsync(id));

    [HttpGet("random")]
    public async Task<ApiResult<IEnumerable<HadithListResponseDto>>> GetRandom([FromQuery] int count = 5)
        => this.ToApiResult(await _hadithService.GetRandomAsync(count));

    #endregion

    #region Admin 

    [HttpPost]
    public async Task<ApiResult<HadithResponseDto>> Create([FromBody] HadithCreateDto dto)
        => this.ToApiResult(await _hadithService.CreateAsync(dto));

    [HttpPut("{id:guid}")]
    public async Task<ApiResult<HadithResponseDto>> Update(Guid id, [FromBody] HadithUpdateDto dto)
        => this.ToApiResult(await _hadithService.UpdateAsync(id, dto));

    [HttpDelete("{id:guid}")]
    public async Task<ApiResult> SoftDelete(Guid id)
        => this.ToApiResult(await _hadithService.SoftDeleteAsync(id));
    [HttpPost("seed")]
    //[Authorize(Roles = "SuperAdmin")] 
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
