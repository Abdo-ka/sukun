namespace Sukun.Application.Dtos.Dua.Response
{
    public class DuaItemListResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string TextEn { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
