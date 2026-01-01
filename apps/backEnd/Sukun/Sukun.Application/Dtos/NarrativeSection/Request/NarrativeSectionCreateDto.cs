namespace Sukun.Application.Dtos.NarrativeSection.Request
{
    public class NarrativeSectionCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
