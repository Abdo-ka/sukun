namespace Sukun.Application.Dtos.Hadith.Request
{
    public class HadithCategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public int Order { get; set; }
    }

}
