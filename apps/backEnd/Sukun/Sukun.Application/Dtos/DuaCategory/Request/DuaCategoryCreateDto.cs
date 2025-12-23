namespace Sukun.Application.Dtos.DuaCategory.Request
{
    public class DuaCategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public int Order { get; set; } = 0;
    }
}
