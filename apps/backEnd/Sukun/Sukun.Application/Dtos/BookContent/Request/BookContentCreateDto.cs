namespace Sukun.Application.Dtos.BookContent.Request
{
    public class BookContentCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int DisplayOrder { get; set; } = 1;
    }
}
