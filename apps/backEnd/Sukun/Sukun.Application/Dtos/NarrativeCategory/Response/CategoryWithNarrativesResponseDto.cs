using Sukun.Application.Dtos.Narrative.Response;

namespace Sukun.Application.Dtos.NarrativeCategory.Response
{
    public class CategoryWithNarrativesResponseDto : CategoryResponseDto
    {
        public IEnumerable<NarrativeListResponseDto> Narratives { get; set; } = new List<NarrativeListResponseDto>();
    }


}
