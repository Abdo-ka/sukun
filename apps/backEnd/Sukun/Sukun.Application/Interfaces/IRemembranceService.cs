using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IRemembranceService
    {
        Task<Result<IEnumerable<RemembranceResponseDto>>> GetByCategoryAsync(Guid categoryId);
        Task<Result<RemembranceResponseDto>> GetByIdAsync(Guid id);
        Task<Result<RemembranceResponseDto>> GetRandomAsync();

        // Admin
        Task<Result<RemembranceResponseDto>> CreateAsync(RemembranceCreateDto dto);
        Task<Result<RemembranceResponseDto>> UpdateAsync(Guid id, RemembranceUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}