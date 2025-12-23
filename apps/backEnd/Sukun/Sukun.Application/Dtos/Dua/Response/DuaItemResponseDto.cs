using Sukun.Application.Dtos.DuaCategory.Response;

namespace Sukun.Application.Dtos.Dua.Response
{

    public class DuaCategoryWithDuasDto : DuaCategoryResponseDto
    {
        public List<DuaItemResponseDto> Duas { get; set; } = new();
    }
}
