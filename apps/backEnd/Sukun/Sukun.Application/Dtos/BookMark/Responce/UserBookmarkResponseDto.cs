using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.BookMark.Responce
{
    // UserBookmark DTOs
    public class UserBookmarkResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VerseId { get; set; }
        public string? Note { get; set; }
        public BookmarkType Type { get; set; }
        public QuranVerseResponseDto Verse { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

