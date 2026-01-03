namespace Sukun.Application.Dtos.Dua.Request
{
    public class DuaItemCreateDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string TextEn { get; set; } = string.Empty;
        public string? Reference { get; set; }
        public string? Virtue { get; set; }
        public int DisplayOrder { get; set; } = 1;
    }
}
