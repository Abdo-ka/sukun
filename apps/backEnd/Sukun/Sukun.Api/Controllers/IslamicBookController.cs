using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.IslamicBook.Response;
using Sukun.Application.Dtos.IslamicBookSection.Request;
using Sukun.Application.Interfaces;
using Sukun.Domin.Enums;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/islamic-books")]
    public class IslamicBookController : ControllerBase
    {
        private readonly IIslamicBookService _bookService;

        public IslamicBookController(IIslamicBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<IslamicBookListDto>>> GetAll()
            => this.ToApiResult(await _bookService.GetAllAsync());

        [HttpGet("type/{type}")]
        public async Task<ApiResult<IEnumerable<IslamicBookListDto>>> GetByType(BookType type)
            => this.ToApiResult(await _bookService.GetByTypeAsync(type));

        [HttpGet("{id:guid}")]
        public async Task<ApiResult<IslamicBookResponseDto>> GetById(Guid id)
            => this.ToApiResult(await _bookService.GetByIdAsync(id));

        [HttpGet("{id:guid}/with-sections")]
        public async Task<ApiResult<IslamicBookResponseDto>> GetByIdWithSections(Guid id)
            => this.ToApiResult(await _bookService.GetByIdWithSectionsAsync(id));

        [HttpPost]
        public async Task<ApiResult<IslamicBookResponseDto>> Create([FromBody] IslamicBookCreateDto dto)
            => this.ToApiResult(await _bookService.CreateAsync(dto));

        [HttpPut("{id:guid}")]
        public async Task<ApiResult<IslamicBookResponseDto>> Update(Guid id, [FromBody] IslamicBookUpdateDto dto)
            => this.ToApiResult(await _bookService.UpdateAsync(id, dto));

        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _bookService.SoftDeleteAsync(id));
    }
}
