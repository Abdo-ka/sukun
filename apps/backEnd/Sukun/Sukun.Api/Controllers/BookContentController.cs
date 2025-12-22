using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.BookContent.Request;
using Sukun.Application.Dtos.BookContent.Response;
using Sukun.Application.Interfaces;
using Sukun.Infrastructure.Abstracts;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/book-contents")]
    public class BookContentController : ControllerBase
    {
        private readonly IBookContentService _contentService;

        public BookContentController(IBookContentService contentService)
        {
            _contentService = contentService;
        }

        // GET: api/book-contents/section/{sectionId:guid}
        [HttpGet("section/{sectionId:guid}")]
        public async Task<ApiResult<IEnumerable<BookContentResponseDto>>> GetBySection(Guid sectionId)
            => this.ToApiResult(await _contentService.GetBySectionAsync(sectionId));

        // GET: api/book-contents/{id:guid}
        [HttpGet("{id:guid}")]
        public async Task<ApiResult<BookContentResponseDto>> GetById(Guid id)
            => this.ToApiResult(await _contentService.GetByIdAsync(id));

        // ====================== Admin Operations ======================

        // POST: api/book-contents/section/{sectionId:guid}
        [HttpPost("section/{sectionId:guid}")]
        public async Task<ApiResult<BookContentResponseDto>> Create(
            Guid sectionId,
            [FromBody] BookContentCreateDto dto)
            => this.ToApiResult(await _contentService.CreateAsync(sectionId, dto));

        // PUT: api/book-contents/{id:guid}
        [HttpPut("{id:guid}")]
        public async Task<ApiResult<BookContentResponseDto>> Update(
            Guid id,
            [FromBody] BookContentUpdateDto dto)
            => this.ToApiResult(await _contentService.UpdateAsync(id, dto));

        // DELETE: api/book-contents/{id:guid} (Soft Delete)
        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _contentService.SoftDeleteAsync(id));
    }
}
