namespace Sukun.Application.Dtos.Remembrance.Response
{
    public class RemembranceListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int RecommendedCount { get; set; }
        public string? Benefits { get; set; }
        public bool IsDaily { get; set; }
    }

}
