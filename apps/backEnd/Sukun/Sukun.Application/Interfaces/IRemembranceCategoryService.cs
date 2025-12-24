using Sukun.Application.Dtos.RemembranceCategory.Request;
using Sukun.Application.Dtos.RemembranceCategory.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IRemembranceCategoryService
    {
        Task<Result<IEnumerable<RemembranceCategoryResponseDto>>> GetAllAsync();
        Task<Result<RemembranceCategoryWithRemembrancesDto>> GetByIdWithRemembrancesAsync(Guid id);

        // Admin
        Task<Result<RemembranceCategoryResponseDto>> CreateAsync(RemembranceCategoryCreateDto dto);
        Task<Result<RemembranceCategoryResponseDto>> UpdateAsync(Guid id, RemembranceCategoryUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}