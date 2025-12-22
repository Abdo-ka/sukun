namespace Sukun.Domin.Entities
{
    public class IslamicBookSection : BaseEntity
    {
        public Guid BookId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; } = 0;

        // Navigation
        public virtual IslamicBook Book { get; set; } = null!;
        public virtual ICollection<Hadith> Hadiths { get; set; } = new List<Hadith>();
        public virtual ICollection<BookContent> Contents { get; set; } = new List<BookContent>();
    }
}




