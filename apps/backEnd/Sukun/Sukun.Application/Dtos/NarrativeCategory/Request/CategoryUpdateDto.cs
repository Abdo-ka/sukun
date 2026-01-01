namespace Sukun.Application.Dtos.NarrativeCategory.Request
{
    public class CategoryUpdateDto
    {
        public string? Title { get; set; }
        public string? TitleAr { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsMainSection { get; set; }
        public int? Order { get; set; }
    }

}
