using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Hadith : BaseEntity
    {
        public Guid CategoryId { get; set; }

        // Hadith Info
        public string Reference { get; set; } // مثل: صحيح البخاري 123
        public string Text { get; set; }
        public string Translation { get; set; }
        public string? Transliteration { get; set; }

        // Grading
        public HadithGrade Grade { get; set; }
        public string? GradedBy { get; set; }
        public string? GradeExplanation { get; set; }

        // Chapters
        public string? BookName { get; set; }
        public string? ChapterName { get; set; }
        public int? HadithNumber { get; set; }

        // Navigation Properties
        public virtual HadithCategory Category { get; set; }
        public virtual ICollection<HadithExplanation> Explanations { get; set; } = new List<HadithExplanation>();
    }

}




