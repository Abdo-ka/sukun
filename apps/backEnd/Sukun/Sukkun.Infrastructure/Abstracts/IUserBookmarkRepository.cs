using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IUserBookmarkRepository : IRepository<UserBookmark>
    {
        Task<UserBookmark?> GetBookmarkAsync(Guid userId, Guid verseId);
        Task<IEnumerable<UserBookmark>> GetBookmarksByUserAsync(Guid userId);
        Task<IEnumerable<UserBookmark>> GetBookmarksByUserAndTypeAsync(Guid userId, BookmarkType type);
        Task<IEnumerable<UserBookmark>> GetBookmarksByVerseAsync(Guid verseId);
        Task<UserBookmark?> GetBookmarkWithDetailsAsync(Guid bookmarkId);
        Task<bool> BookmarkExistsAsync(Guid userId, Guid verseId);
        Task<Result<UserBookmark>> UpdateNoteAsync(Guid bookmarkId, string note);
        Task<Result<UserBookmark>> ChangeBookmarkTypeAsync(Guid bookmarkId, BookmarkType newType);
        Task<int> CountBookmarksByUserAsync(Guid userId);
        Task<Dictionary<BookmarkType, int>> GetBookmarkStatsByUserAsync(Guid userId);
        Task<IEnumerable<UserBookmark>> GetRecentBookmarksByUserAsync(Guid userId, int count);
        Task<Result<int>> RemoveAllBookmarksByUserAsync(Guid userId);
        Task<Result<int>> RemoveBookmarksByVerseAsync(Guid verseId);
    }
}
