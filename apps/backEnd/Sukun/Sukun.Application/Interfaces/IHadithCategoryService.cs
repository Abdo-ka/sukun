using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IHadithCategoryService
        {
            Task<Result<IEnumerable<HadithCategoryResponseDto>>> GetAllAsync();
            Task<Result<HadithCategoryResponseDto>> GetByIdAsync(Guid id);
            Task<Result<int>> GetHadithsCountAsync(Guid categoryId);

            // Admin
            Task<Result<HadithCategoryResponseDto>> CreateAsync(HadithCategoryCreateDto dto);
            Task<Result<HadithCategoryResponseDto>> UpdateAsync(Guid id, HadithCategoryUpdateDto dto);
            Task<Result> SoftDeleteAsync(Guid id);
        }
    }
