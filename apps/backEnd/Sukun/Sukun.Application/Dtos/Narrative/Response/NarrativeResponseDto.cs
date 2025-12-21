using Sukun.Domin.Entities;

namespace Sukun.Application.Dtos.Narrative.Response
{
    public class NarrativeResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public Guid? ParentId { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }
        public IEnumerable<NarrativeResponseDto> Children { get; set; }
        public IEnumerable<NarrativeSectionResponseDto> Sections { get; set; } = new List<NarrativeSectionResponseDto>();
    }
}
