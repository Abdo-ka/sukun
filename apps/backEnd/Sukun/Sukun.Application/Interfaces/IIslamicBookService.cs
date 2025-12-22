using Sukun.Application.Dtos.IslamicBook.Response;
using Sukun.Application.Dtos.IslamicBookSection.Request;
using Sukun.Domin.Common;
using Sukun.Domin.Enums;

namespace Sukun.Application.Interfaces
{
    public interface IIslamicBookService
    {
        Task<Result<IEnumerable<IslamicBookListDto>>> GetAllAsync();
        Task<Result<IEnumerable<IslamicBookListDto>>> GetByTypeAsync(BookType type);
        Task<Result<IslamicBookResponseDto>> GetByIdAsync(Guid id);
        Task<Result<IslamicBookResponseDto>> GetByIdWithSectionsAsync(Guid id);

        // Admin
        Task<Result<IslamicBookResponseDto>> CreateAsync(IslamicBookCreateDto dto);
        Task<Result<IslamicBookResponseDto>> UpdateAsync(Guid id, IslamicBookUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}