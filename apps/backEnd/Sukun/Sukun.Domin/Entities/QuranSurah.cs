using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class QuranSurah : BaseEntity
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public int RevelationOrder { get; set; }
        public RevelationType RevelationType { get; set; } // Makki, Madani
        public int TotalVerses { get; set; }

        // Navigation Properties
        public virtual ICollection<QuranVerse> Verses { get; set; } = new List<QuranVerse>();
    }
}




