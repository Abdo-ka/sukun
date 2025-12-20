using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.BookMark.Request
{
    public class UserBookmarkUpdateDto
    {
        public string? Note { get; set; }
        public BookmarkType? Type { get; set; }
    }
}

