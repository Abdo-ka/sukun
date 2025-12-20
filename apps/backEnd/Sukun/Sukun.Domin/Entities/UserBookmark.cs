using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class UserBookmark : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid VerseId { get; set; }

        // Bookmark Info
        public string? Note { get; set; }
        public BookmarkType Type { get; set; } // Favorite, Memorize, Understand

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual QuranVerse Verse { get; set; }
    }
}




