using Sukun.Domin.Entities;

namespace Sukun.Application.Dtos.Narrative.Request
{
    public class NarrativeCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public Guid? ParentId { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public List<NarrativeSectionCreateDto> Sections { get; set; } = new();
    }
}
