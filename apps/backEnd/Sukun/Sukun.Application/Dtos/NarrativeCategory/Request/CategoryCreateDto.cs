namespace Sukun.Application.Dtos.NarrativeCategory.Request
{
    public class CategoryCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsMainSection { get; set; } = false;
        public int Order { get; set; } = 0;
    }

}
