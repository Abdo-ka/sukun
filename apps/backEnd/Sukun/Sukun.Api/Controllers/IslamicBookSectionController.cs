using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.IslamicBook.Request;
using Sukun.Application.Dtos.IslamicBookSection.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/islamic-book-sections")]
    public class IslamicBookSectionController : ControllerBase
    {
        private readonly IIslamicBookSectionService _sectionService;

        public IslamicBookSectionController(IIslamicBookSectionService sectionService)
        {
            _sectionService = sectionService;
        }

        // GET: api/islamic-book-sections/book/{bookId:guid}
        [HttpGet("book/{bookId:guid}")]
        public async Task<ApiResult<IEnumerable<IslamicBookSectionResponseDto>>> GetByBook(Guid bookId)
            => this.ToApiResult(await _sectionService.GetByBookAsync(bookId));

        // GET: api/islamic-book-sections/{id:guid}
        [HttpGet("{id:guid}")]
        public async Task<ApiResult<IslamicBookSectionResponseDto>> GetById(Guid id)
            => this.ToApiResult(await _sectionService.GetByIdAsync(id));

        // ====================== Admin Operations ======================

        // POST: api/islamic-book-sections/book/{bookId:guid}
        [HttpPost("book/{bookId:guid}")]
        public async Task<ApiResult<IslamicBookSectionResponseDto>> Create(
            Guid bookId,
            [FromBody] IslamicBookSectionCreateDto dto)
            => this.ToApiResult(await _sectionService.CreateAsync(bookId, dto));

        // PUT: api/islamic-book-sections/{id:guid}
        [HttpPut("{id:guid}")]
        public async Task<ApiResult<IslamicBookSectionResponseDto>> Update(
            Guid id,
            [FromBody] IslamicBookSectionUpdateDto dto)
            => this.ToApiResult(await _sectionService.UpdateAsync(id, dto));

        // DELETE: api/islamic-book-sections/{id:guid} (Soft Delete)
        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _sectionService.SoftDeleteAsync(id));
    }
}
