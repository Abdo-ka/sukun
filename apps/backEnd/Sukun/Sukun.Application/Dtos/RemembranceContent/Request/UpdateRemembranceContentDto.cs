using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.RemembranceContent.Request
{
    public class UpdateRemembranceContentDto
    {
        public SourceType SourceType { get; set; }

        public Guid? SourceId { get; set; }

        public string? CustomContent { get; set; }
    }
}
