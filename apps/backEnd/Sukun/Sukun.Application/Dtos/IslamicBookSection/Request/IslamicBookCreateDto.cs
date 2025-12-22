using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.IslamicBookSection.Request
{
    public class IslamicBookCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public BookType Type { get; set; } = BookType.Hadith;
        public int Order { get; set; }
        public string? IconUrl { get; set; }
    }
}
