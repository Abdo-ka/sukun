namespace Sukun.Application.Dtos.Dua.Response
{
    public class DuaItemListResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ArabicText { get; set; } = string.Empty;
        public int? RepeatCount { get; set; }
        public int DisplayOrder { get; set; }
    }
}
