using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.IslamicBook.Response
{
    public class IslamicBookListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public BookType Type { get; set; }
        public string? Author { get; set; }
        public string? IconUrl { get; set; }
        public int Order { get; set; }
        public int HadithsCount { get; set; }
    }
}
