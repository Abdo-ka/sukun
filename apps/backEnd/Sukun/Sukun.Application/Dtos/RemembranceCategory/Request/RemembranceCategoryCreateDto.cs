namespace Sukun.Application.Dtos.RemembranceCategory.Request
{
    public class RemembranceCategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public bool IsDaily { get; set; }
    }

}
