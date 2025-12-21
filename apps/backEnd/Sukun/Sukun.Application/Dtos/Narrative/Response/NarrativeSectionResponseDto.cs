namespace Sukun.Application.Dtos.Narrative.Response
{
    public class NarrativeSectionResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public string? MediaUrl { get; set; }
    }
}
