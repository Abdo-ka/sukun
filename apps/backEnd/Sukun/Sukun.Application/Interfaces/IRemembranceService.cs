using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IRemembranceService
    {
        Task<Result<IEnumerable<RemembranceResponseDto>>> GetByCategoryAsync(Guid categoryId);
        Task<Result<RemembranceResponseDto>> GetRandomAsync();
        Task<Result<List<RemembranceResponseDto>>> GetAllWithFullContentAsync();
        Task<Result<PagedResponseDto<RemembranceResponseDto>>> GetPagedWithFullContentAsync(PagedRequestDto request,Guid? categoryId = null);
        Task<Result<RemembranceResponseDto>> GetByIdWithFullContentAsync(Guid id);
        Task<Result<RemembranceResponseDto>> CreateAsync(RemembranceCreateDto dto);
        Task<Result<RemembranceResponseDto>> UpdateAsync(Guid id, RemembranceUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}