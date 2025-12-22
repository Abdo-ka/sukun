using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Hadith.Request
{
    public class HadithCreateDto
    {
        public Guid CategoryId { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public HadithGrade Grade { get; set; }
        public string? GradedBy { get; set; }
        public string? GradeExplanation { get; set; }
        public string? BookName { get; set; }
        public string? ChapterName { get; set; }
        public int? HadithNumber { get; set; }
        public List<HadithExplanationCreateDto>? Explanations { get; set; }
    }

}
