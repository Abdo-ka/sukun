using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Hadith.Request
{
    public class HadithCreateDto
    {
        public Guid CategoryId { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public HadithGrade Grade { get; set; }
        public string? HadithNumber { get; set; }
        public List<HadithExplanationCreateDto>? Explanations { get; set; }
    }

}
