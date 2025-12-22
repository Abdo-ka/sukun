using Sukun.Application.Dtos.IslamicBook.Request;
using Sukun.Application.Dtos.IslamicBookSection.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IIslamicBookSectionService
    {
        Task<Result<IEnumerable<IslamicBookSectionResponseDto>>> GetByBookAsync(Guid bookId);
        Task<Result<IslamicBookSectionResponseDto>> GetByIdAsync(Guid id);

        // Admin
        Task<Result<IslamicBookSectionResponseDto>> CreateAsync(Guid bookId, IslamicBookSectionCreateDto dto);
        Task<Result<IslamicBookSectionResponseDto>> UpdateAsync(Guid id, IslamicBookSectionUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}