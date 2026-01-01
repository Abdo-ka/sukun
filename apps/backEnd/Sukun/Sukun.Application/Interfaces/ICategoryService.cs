using Sukun.Application.Dtos.NarrativeCategory.Request;
using Sukun.Application.Dtos.NarrativeCategory.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryResponseDto>>> GetAllAsync();
        Task<Result<IEnumerable<CategoryResponseDto>>> GetMainSectionsAsync();
        Task<Result<IEnumerable<CategoryResponseDto>>> GetRootAsync();
        Task<Result<CategoryWithChildrenResponseDto>> GetByIdWithChildrenAsync(Guid id);
        Task<Result<CategoryWithNarrativesResponseDto>> GetByIdWithNarrativesAsync(Guid id);

        Task<Result<CategoryResponseDto>> CreateAsync(CategoryCreateDto dto);
        Task<Result<CategoryResponseDto>> UpdateAsync(Guid id, CategoryUpdateDto dto);
        Task<Result<bool>> SoftDeleteAsync(Guid id);
    }
}