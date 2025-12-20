using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.BookMark.Request
{
    // UserBookmark
    public class UserBookmarkCreateDto
    {
        public Guid VerseId { get; set; }
        public string? Note { get; set; }
        public BookmarkType Type { get; set; } = BookmarkType.Favorite;
    }
}

