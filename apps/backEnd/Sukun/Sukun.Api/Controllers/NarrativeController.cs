using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Application.Dtos.NarrativeSection.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]s")]
public class NarrativeController : ControllerBase
{
    private readonly INarrativeService _narrativeService;

    public NarrativeController(INarrativeService narrativeService)
    {
        _narrativeService = narrativeService;
    }
    
    [HttpGet("Details")]
    public async Task<ApiResult<IEnumerable<NarrativeResponseDto>>> GetAllWithFullContent(ContentType? type = null, Guid? tagId = null, Guid? categoryId = null)
        => this.ToApiResult(await _narrativeService.GetAllWithFullContent(type, tagId, categoryId));
  
    [HttpGet("{id:guid}/Details")]
    public async Task<ApiResult<NarrativeResponseDto>> GetByIdWithFullDetailsAsync(Guid id)
 => this.ToApiResult(await _narrativeService.GetByIdWithFullDetailsAsync(id));

    [HttpGet("paged")]
    public async Task<ApiResult<PagedResponseDto<NarrativeResponseDto>>> GetPaged([FromQuery] PagedRequestDto request, ContentType? type = null, Guid? tagId = null, Guid? categoryId = null)
        => this.ToApiResult(await _narrativeService.GetPagedAsync(request, type, tagId, categoryId));
 
    [HttpGet]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetAll()
        => this.ToApiResult(await _narrativeService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<ApiResult<NarrativeListResponseDto>> GetById(Guid id)
  => this.ToApiResult(await _narrativeService.GetByIdAsync(id));

    [HttpGet("featured")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetFeatured([FromQuery] int count = 10)
     => this.ToApiResult(await _narrativeService.GetFeaturedAsync(count));

    [HttpGet("type")]
    public async Task<ApiResult<IEnumerable<NarrativeListResponseDto>>> GetByType(ContentType type)
        => this.ToApiResult(await _narrativeService.GetByTypeAsync(type));

    [HttpPost]
    //[Authorize(Roles = "Admin")] 
    public async Task<ApiResult<NarrativeResponseDto>> Create([FromBody] NarrativeCreateDto dto)
    => this.ToApiResult(await _narrativeService.CreateAsync(dto));

    [HttpPut("{id:guid}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResult<NarrativeResponseDto>> Update(Guid id, [FromBody] NarrativeUpdateDto dto)
         => this.ToApiResult(await _narrativeService.UpdateAsync(id, dto));
 

    [HttpDelete("{id:guid}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResult<bool>> SoftDelete(Guid id)
    => this.ToApiResult(await _narrativeService.SoftDeleteAsync(id));

   

}

