using Sukun.Application.Dtos.Tag.Request;
using Sukun.Application.Dtos.Tag.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class TagMapper
    {
        // في NarrativeMapper أو TagMapper
        public static TagResponseDto ToResponseDto(this Tag tag)
        {
            return new TagResponseDto
            {
                Id = tag.Id,
                Name = tag.Name,
                NameAr = tag.NameAr,
               
            };
        }

        public static Tag ToEntity(this TagCreateDto dto)
        {
            return new Tag
            {
                Name = dto.Name.Trim(),
                NameAr = dto.NameAr?.Trim()
            };
        }
    }
}
