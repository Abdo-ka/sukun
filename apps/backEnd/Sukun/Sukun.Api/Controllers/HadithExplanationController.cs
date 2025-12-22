using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Application.Implemantation;

[ApiController]
[Route("api/hadith-explanations")] // مسار بسيط وواضح
public class HadithExplanationController : ControllerBase
{
    private readonly IHadithExplanationService _explanationService;

    public HadithExplanationController(IHadithExplanationService explanationService)
    {
        _explanationService = explanationService;
    }

    // GET: api/hadith-explanations/hadith/{hadithId:guid}
    [HttpGet("hadith/{hadithId:guid}")]
    public async Task<ApiResult<IEnumerable<HadithExplanationResponseDto>>> GetByHadith(Guid hadithId)
        => this.ToApiResult(await _explanationService.GetByHadithAsync(hadithId));

    // GET: api/hadith-explanations/{id:guid}
    [HttpGet("{id:guid}")]
    public async Task<ApiResult<HadithExplanationResponseDto>> GetById(Guid id)
        => this.ToApiResult(await _explanationService.GetByIdAsync(id));

    // POST: api/hadith-explanations/hadith/{hadithId:guid}
    [HttpPost("hadith/{hadithId:guid}")]
    public async Task<ApiResult<HadithExplanationResponseDto>> Create(
        Guid hadithId,
        [FromBody] HadithExplanationCreateDto dto)
        => this.ToApiResult(await _explanationService.CreateAsync(hadithId, dto));

    // PUT: api/hadith-explanations/{id:guid}
    [HttpPut("{id:guid}")]
    public async Task<ApiResult<HadithExplanationResponseDto>> Update(
        Guid id,
        [FromBody] HadithExplanationUpdateDto dto)
        => this.ToApiResult(await _explanationService.UpdateAsync(id, dto));

    // DELETE: api/hadith-explanations/{id:guid}
    [HttpDelete("{id:guid}")]
    public async Task<ApiResult> Delete(Guid id)
        => this.ToApiResult(await _explanationService.DeleteAsync(id));
}