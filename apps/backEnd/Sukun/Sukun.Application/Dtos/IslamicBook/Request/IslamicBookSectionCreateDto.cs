namespace Sukun.Application.Dtos.IslamicBook.Request
{
    public class IslamicBookSectionCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; } = 0;
    }
}
