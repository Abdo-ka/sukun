using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IHadithService
    {
        // Categories
        Task<Result<IEnumerable<HadithCategoryResponseDto>>> GetAllCategoriesAsync();

        // Hadiths
        Task<Result<PagedResponseDto<HadithListResponseDto>>> GetPagedAsync(PagedRequestDto request, Guid? bookId = null);
        Task<Result<IEnumerable<HadithListResponseDto>>> GetByCategoryAsync(Guid categoryId);
        Task<Result<HadithResponseDto>> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<HadithListResponseDto>>> GetRandomAsync(int count = 5);
        Task<Result<PagedResponseDto<HadithListResponseDto>>> SearchAsync(string query, PagedRequestDto? paging = null);

        // Admin CRUD
        Task<Result<HadithResponseDto>> CreateAsync(HadithCreateDto dto);
        Task<Result<HadithResponseDto>> UpdateAsync(Guid id, HadithUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}