using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Hadith.Response
{
    public class HadithListResponseDto
    {
        public Guid Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public HadithGrade Grade { get; set; }
        public Guid? BookId { get; set; }
        public string? BookName { get; set; }
        public Guid? SectionId { get; set; }
        public string? SectionName { get; set; }
        public string? HadithNumber { get; set; }
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

}
