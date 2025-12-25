using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Hadith : BaseEntity
    {

        public string? Reference { get; set; } 
        public string Text { get; set; }
        public string? HeadingArabic { get; set; }
        public HadithGrade Grade { get; set; }
        public string? HadithNumber { get; set; }

        public Guid? BookId { get; set; }
        public Guid? SectionId { get; set; }
        public Guid? CategoryId { get; set; }

        // Navigation
        public virtual IslamicBook Book { get; set; } = null!;
        public virtual IslamicBookSection? Section { get; set; }
        public virtual HadithCategory? Category { get; set; }
        public virtual ICollection<HadithExplanation> Explanations { get; set; } = new List<HadithExplanation>();
    }

}




