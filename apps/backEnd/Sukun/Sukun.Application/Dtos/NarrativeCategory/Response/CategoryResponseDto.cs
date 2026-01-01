namespace Sukun.Application.Dtos.NarrativeCategory.Response
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public int Order { get; set; }
        public bool IsMainSection { get; set; }
        public Guid? ParentId { get; set; }
    }

}
