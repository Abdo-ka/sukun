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
        public Guid BookId { get; set; }
        public Guid? SectionId { get; set; }

        // Navigation
        public virtual IslamicBook Book { get; set; } = null!;
        public virtual IslamicBookSection? Section { get; set; }
        public virtual HadithCategory Category { get; set; }
        public virtual ICollection<HadithExplanation> Explanations { get; set; } = new List<HadithExplanation>();
    }

}




