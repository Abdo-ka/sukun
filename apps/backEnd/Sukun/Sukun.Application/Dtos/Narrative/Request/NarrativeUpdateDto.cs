using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Narrative.Request
{
    public class NarrativeUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public bool IsFeatured { get; set; } = false;
        public List<NarrativeSectionUpdateDto> Sections { get; set; } = new();
        public List<Guid> TagIds { get; set; } = new();
        public List<Guid> CategoryIds { get; set; } = new();
    }
}
