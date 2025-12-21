using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Domin.Entities;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]s")] // ينتج /api/narratives
public class NarrativeController : ControllerBase
{
    private readonly INarrativeService _narrativeService;

    public NarrativeController(INarrativeService narrativeService)
    {
        _narrativeService = narrativeService;
    }

    #region Narrative CRUD & Queries

    [HttpGet]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetAll()
        => this.ToApiResult(await _narrativeService.GetAllAsync());

    [HttpGet("root")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetRoot()
        => this.ToApiResult(await _narrativeService.GetRootAsync());

    [HttpGet("featured")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetFeatured()
        => this.ToApiResult(await _narrativeService.GetFeaturedAsync());

    [HttpGet("type")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetByType(ContentType type)
        => this.ToApiResult(await _narrativeService.GetByTypeAsync(type));

    [HttpGet("{id:guid}/children")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetChildren(Guid id)
        => this.ToApiResult(await _narrativeService.GetChildrenAsync(id));

    [HttpGet("{id:guid}")]
    public async Task<ApiResult<NarrativeResponseDto>> GetById(Guid id)
        => this.ToApiResult(await _narrativeService.GetByIdWithFullDetailsAsync(id));

    [HttpPost]
    public async Task<ApiResult<NarrativeResponseDto>> Create([FromBody] NarrativeCreateDto dto)
        => this.ToApiResult(await _narrativeService.CreateAsync(dto));

    [HttpPut("{id:guid}")]
    public async Task<ApiResult<NarrativeResponseDto>> Update(Guid id, [FromBody] NarrativeUpdateDto dto)
        => this.ToApiResult(await _narrativeService.UpdateAsync(id, dto));

    [HttpDelete("{id:guid}")]
    public async Task<ApiResult> SoftDelete(Guid id)
        => this.ToApiResult(await _narrativeService.SoftDeleteAsync(id));
    
    [HttpGet("paged")]
    public async Task<ApiResult<PagedResponseDto<NarrativeListResponseDto>>> GetPaged([FromQuery] PagedRequestDto request)
        => this.ToApiResult(await _narrativeService.GetPagedAsync(request));
    #endregion

    #region NarrativeSection Operations (Sub-resource)

    [HttpPost("{narrativeId:guid}/sections")]
    public async Task<ApiResult<NarrativeSectionResponseDto>> AddSection(
        Guid narrativeId,
        [FromBody] NarrativeSectionCreateDto dto)
        => this.ToApiResult(await _narrativeService.AddSectionAsync(narrativeId, dto));

    [HttpGet("sections/{sectionId:guid}")]
    public async Task<ApiResult<NarrativeSectionResponseDto>> GetSection(Guid sectionId)
        => this.ToApiResult(await _narrativeService.GetSectionByIdAsync(sectionId));

    [HttpPut("sections/{sectionId:guid}")]
    public async Task<ApiResult<NarrativeSectionResponseDto>> UpdateSection(
        Guid sectionId,
        [FromBody] NarrativeSectionUpdateDto dto)
        => this.ToApiResult(await _narrativeService.UpdateSectionAsync(sectionId, dto));

    [HttpDelete("sections/{sectionId:guid}")]
    public async Task<ApiResult> SoftDeleteSection(Guid sectionId)
        => this.ToApiResult(await _narrativeService.SoftDeleteSectionAsync(sectionId));

    #endregion
    
    [HttpGet("prophet-life-sections")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetProphetLifeSections()
    => this.ToApiResult(await _narrativeService.GetProphetLifeMainSectionsAsync());

    [HttpGet("prophet-battles")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetProphetBattles()
        => this.ToApiResult(await _narrativeService.GetProphetBattlesAsync());

    [HttpGet("prophet-wives")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetProphetWives()
        => this.ToApiResult(await _narrativeService.GetProphetWivesAsync());

    [HttpGet("companions")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetCompanions()
        => this.ToApiResult(await _narrativeService.GetCompanionsStoriesAsync());

    [HttpGet("prophets-stories")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetProphetsStories()
        => this.ToApiResult(await _narrativeService.GetProphetsStoriesAsync());
}