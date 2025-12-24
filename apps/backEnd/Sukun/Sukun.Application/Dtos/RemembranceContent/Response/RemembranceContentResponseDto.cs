using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.RemembranceContent.Response
{
    public class RemembranceContentResponseDto
    {
        public SourceType SourceType { get; set; }
        public Guid? SourceId { get; set; }
        public string? CustomContent { get; set; }
        public string DisplayText { get; set; } = string.Empty; // النص النهائي للعرض (من Custom أو المصدر)
    }

}
