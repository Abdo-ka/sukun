using Sukun.Application.Dtos.Tag.Request;
using Sukun.Application.Dtos.Tag.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface ITagService
    {
        Task<Result<IEnumerable<TagResponseDto>>> GetAllAsync();
        Task<Result<PagedResponseDto<TagResponseDto>>> GetPagedAsync(PagedRequestDto requestDto);
        Task<Result<TagResponseDto>> GetByIdAsync(Guid id);
        Task<Result<TagResponseDto>> CreateAsync(TagCreateDto dto);
        Task<Result<TagResponseDto>> UpdateAsync(Guid id, TagUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
        Task<Result<IEnumerable<TagResponseDto>>> SearchAsync(string query); 
    }
}