namespace Sukun.Application.Dtos.NarrativeSection.Request
{
    public class NarrativeSectionUpdateDto
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
