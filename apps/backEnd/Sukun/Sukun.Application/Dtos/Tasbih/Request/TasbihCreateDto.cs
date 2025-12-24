namespace Sukun.Application.Dtos.Tasbih.Request
{
    public class TasbihCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string TitleAr { get; set; } = string.Empty;
        public string? Benefits { get; set; }
        public int RecommendedCount { get; set; } = 100;
        public int Order { get; set; } = 0;
    }
}
