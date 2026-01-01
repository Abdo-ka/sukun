namespace Sukun.Application.Dtos.NarrativeCategory.Response
{
    public class CategoryWithChildrenResponseDto : CategoryResponseDto
    {
        public List<CategoryWithChildrenResponseDto> Children { get; set; } = new();
    }

}
