using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Application.Dtos.RemembranceCategory.Response;

namespace Sukun.Application.Dtos.RemembranceCategory.Request
{
    public class RemembranceCategoryWithRemembrancesDto : RemembranceCategoryResponseDto
    {
        public List<RemembranceResponseDto> Remembrances { get; set; } = new();
    }

}
