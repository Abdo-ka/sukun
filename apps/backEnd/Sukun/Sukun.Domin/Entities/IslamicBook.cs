using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class IslamicBook : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public BookType Type { get; set; } = BookType.Hadith;
        public int Order { get; set; } = 0;

        // Navigation
        public virtual ICollection<IslamicBookSection> Sections { get; set; } = new List<IslamicBookSection>();
        public virtual ICollection<Hadith> Hadiths { get; set; } = new List<Hadith>();
    }
}




