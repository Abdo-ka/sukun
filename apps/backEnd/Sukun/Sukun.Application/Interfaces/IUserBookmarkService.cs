using Sukun.Application.Dtos.BookMark.Request;
using Sukun.Application.Dtos.BookMark.Responce;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;
using Sukun.Domin.Enums;

namespace Sukun.Application.Interfaces
{
    public interface IUserBookmarkService
    {
        Task<Result<UserBookmarkResponseDto>> GetByIdAsync(Guid bookmarkId);
        Task<Result<UserBookmarkResponseDto>> GetBookmarkAsync(Guid userId, Guid verseId);
        Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetBookmarksByUserAsync(Guid userId);
        Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetBookmarksByUserAndTypeAsync(Guid userId, BookmarkType type);
        Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetBookmarksByVerseAsync(Guid verseId);
        Task<Result<UserBookmarkResponseDto>> GetBookmarkWithDetailsAsync(Guid bookmarkId);
        Task<Result<bool>> BookmarkExistsAsync(Guid userId, Guid verseId);
        Task<Result<UserBookmarkResponseDto>> UpdateNoteAsync(Guid bookmarkId, string note);
        Task<Result<UserBookmarkResponseDto>> ChangeBookmarkTypeAsync(Guid bookmarkId, BookmarkType newType);
        Task<Result<int>> CountBookmarksByUserAsync(Guid userId);
        Task<Result<Dictionary<BookmarkType, int>>> GetBookmarkStatsByUserAsync(Guid userId);
        Task<Result<IEnumerable<UserBookmarkResponseDto>>> GetRecentBookmarksByUserAsync(Guid userId, int count = 10);
        Task<Result<int>> RemoveAllBookmarksByUserAsync(Guid userId);
        Task<Result<int>> RemoveBookmarksByVerseAsync(Guid verseId);
        Task<Result<UserBookmarkResponseDto>> CreateBookmarkAsync(Guid userId, UserBookmarkCreateDto dto);
        Task<Result<PagedResponseDto<UserBookmarkResponseDto>>> GetBookmarksByUserPagedAsync(Guid userId, PagedRequestDto request);
        Task<Result<UserBookmarkResponseDto>> UpdateBookmarkAsync(Guid bookmarkId, UserBookmarkUpdateDto dto);
        Task<Result<bool>> DeleteBookmarkAsync(Guid bookmarkId);
        Task<Result<UserBookmarkResponseDto>> UpdateBookmarkNoteAsync(Guid bookmarkId, string note);
    }
}