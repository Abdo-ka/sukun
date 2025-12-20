using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Tafsir : BaseEntity
    {
        public Guid? VerseId { get; set; }

        // Tafsir Info
        public TafsirSource Source { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        // Navigation Properties
        public virtual QuranVerse? Verse { get; set; }
    }
}




