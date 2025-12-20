namespace Sukun.Domin.Entities
{
    public class QuranVerse : BaseEntity
    {
        public Guid SurahId { get; set; }
        public int VerseNumber { get; set; }
        public int PageNumber { get; set; }
        public int JuzNumber { get; set; }
        public int HizbNumber { get; set; }
        public string Text { get; set; }

        // Navigation Properties
        public virtual QuranSurah Surah { get; set; }
        public virtual ICollection<Tafsir> Tafsirs { get; set; } = new List<Tafsir>();
    }
}




