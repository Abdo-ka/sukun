namespace Sukun.Application.Dtos.Dua.Request
{
    public class DuaItemCreateDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ArabicText { get; set; } = string.Empty;
        public string? Transliteration { get; set; }
        public string? Translation { get; set; }
        public string? Reference { get; set; }
        public int? RepeatCount { get; set; }
        public string? Virtue { get; set; }
        public int DisplayOrder { get; set; } = 1;
    }
}
