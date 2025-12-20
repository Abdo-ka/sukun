using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.BookMark.Request;
using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/bookmarks")]
    public class UserBookmarkController : ControllerBase
    {
        private readonly IUserBookmarkService _bookmarkService;

        public UserBookmarkController(IUserBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpPost]
        public async Task<ApiResult<UserBookmarkResponseDto>> Create(Guid userId, [FromBody] UserBookmarkCreateDto bookmarkDto)
            => this.ToApiResult(await _bookmarkService.CreateBookmarkAsync(userId,bookmarkDto));

        [HttpGet]
        public async Task<ApiResult<IEnumerable<UserBookmarkResponseDto>>> GetAll([FromQuery] Guid userId)
            => this.ToApiResult(await _bookmarkService.GetBookmarksByUserAsync(userId));

        [HttpGet("paged")]
        public async Task<ApiResult<PagedResponseDto<UserBookmarkResponseDto>>> GetPaged([FromQuery] Guid userId, [FromQuery] PagedRequestDto request)
            => this.ToApiResult(await _bookmarkService.GetBookmarksByUserPagedAsync(userId, request));

        [HttpGet("type/{type}")]
        public async Task<ApiResult<IEnumerable<UserBookmarkResponseDto>>> GetByType([FromQuery] Guid userId, BookmarkType type)
            => this.ToApiResult(await _bookmarkService.GetBookmarksByUserAndTypeAsync(userId, type));

        [HttpGet("{bookmarkId:guid}")]
        public async Task<ApiResult<UserBookmarkResponseDto>> GetById(Guid bookmarkId)
            => this.ToApiResult(await _bookmarkService.GetByIdAsync(bookmarkId));

        [HttpPut("{bookmarkId:guid}")]
        public async Task<ApiResult<UserBookmarkResponseDto>> Update(Guid bookmarkId, [FromBody] UserBookmarkUpdateDto request)
            => this.ToApiResult(await _bookmarkService.UpdateBookmarkAsync(bookmarkId, request));

        [HttpDelete("{bookmarkId:guid}")]
        public async Task<ApiResult<bool>> Delete(Guid bookmarkId)
            => this.ToApiResult(await _bookmarkService.DeleteBookmarkAsync(bookmarkId));

        [HttpPatch("{bookmarkId:guid}/note")]
        public async Task<ApiResult<UserBookmarkResponseDto>> UpdateNote(Guid bookmarkId, [FromBody] UpdateNoteRequest request)
            => this.ToApiResult(await _bookmarkService.UpdateBookmarkNoteAsync(bookmarkId, request.Note));

        [HttpPatch("{bookmarkId:guid}/type")]
        public async Task<ApiResult<UserBookmarkResponseDto>> ChangeType(Guid bookmarkId, [FromBody] ChangeTypeRequest request)
            => this.ToApiResult(await _bookmarkService.ChangeBookmarkTypeAsync(bookmarkId, request.NewType));

        [HttpGet("stats")]
        public async Task<ApiResult<Dictionary<BookmarkType, int>>> GetStats([FromQuery] Guid userId)
            => this.ToApiResult(await _bookmarkService.GetBookmarkStatsByUserAsync(userId));

        [HttpGet("recent")]
        public async Task<ApiResult<IEnumerable<UserBookmarkResponseDto>>> GetRecent([FromQuery] Guid userId, [FromQuery] int count = 10)
            => this.ToApiResult(await _bookmarkService.GetRecentBookmarksByUserAsync(userId, count));

        [HttpDelete("all")]
        public async Task<ApiResult<int>> DeleteAll([FromQuery] Guid userId)
            => this.ToApiResult(await _bookmarkService.RemoveAllBookmarksByUserAsync(userId));
    }


}
