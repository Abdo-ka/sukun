using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Tag.Request;
using Sukun.Application.Dtos.Tag.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")] 
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ApiResult<IEnumerable<TagResponseDto>>> GetAll()
        => this.ToApiResult(await _tagService.GetAllAsync());

    [HttpGet("paged")]
    public async Task<ApiResult<PagedResponseDto<TagResponseDto>>> GetPaged(
        [FromQuery] PagedRequestDto requestDto)
        => this.ToApiResult(await _tagService.GetPagedAsync(requestDto));

    [HttpGet("{id:guid}")]
    public async Task<ApiResult<TagResponseDto>> GetById(Guid id)
        => this.ToApiResult(await _tagService.GetByIdAsync(id));

    [HttpGet("search")]
    public async Task<ApiResult<IEnumerable<TagResponseDto>>> Search([FromQuery] string q)
        => this.ToApiResult(await _tagService.SearchAsync(q));

    [HttpPost]
    public async Task<ApiResult<TagResponseDto>> Create([FromBody] TagCreateDto dto)
        => this.ToApiResult(await _tagService.CreateAsync(dto));

    [HttpPut("{id:guid}")]
    public async Task<ApiResult<TagResponseDto>> Update(Guid id, [FromBody] TagUpdateDto dto)
        => this.ToApiResult(await _tagService.UpdateAsync(id, dto));

    [HttpDelete("{id:guid}")]
    public async Task<ApiResult> Delete(Guid id)
        => this.ToApiResult(await _tagService.SoftDeleteAsync(id));
}

