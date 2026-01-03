using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.RemembranceContent.Response
{
    public class RemembranceContentResponseDto
    {
        public Guid Id { get; set; }
        public SourceType SourceType { get; set; }
        public Guid? SourceId { get; set; }
        public string? CustomContent { get; set; }
        public string DisplayText { get; set; } = string.Empty;

        public string? Reference { get; set; }
        public string? DisplayTextEn { get; set; }
        public string? Meaning { get; set; }
        public string? MeaningEn { get; set; }
        public string? Benefits { get; set; }
    }
}
