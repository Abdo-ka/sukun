using Sukun.Application.Dtos.BookContent.Response;

namespace Sukun.Application.Dtos.IslamicBookSection.Response
{
    public class IslamicBookSectionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
        public int HadithsCount { get; set; }
        public List<BookContentResponseDto> Contents { get; set; } = new();
    }
}
