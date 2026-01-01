using Sukun.Application.Dtos.NarrativeCategory.Response;
using Sukun.Application.Dtos.NarrativeSection.Response;
using Sukun.Application.Dtos.Tag.Response;
using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Narrative.Response
{
    public class NarrativeResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<NarrativeSectionResponseDto> Sections { get; set; } = new();
        public List<TagResponseDto> Tags { get; set; } = new(); 
        public List<CategoryResponseDto> Categories { get; set; } = new(); 
     }
 
}
