using Sukun.Application.Dtos.RemembranceContent.Request;

namespace Sukun.Application.Dtos.Remembrance.Request
{
    public class RemembranceCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int RecommendedCount { get; set; } = 1;
        public string? Benefits { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
        public List<RemembranceContentCreateDto> Contents { get; set; } = new();
    }

}
