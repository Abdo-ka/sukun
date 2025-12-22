using Sukun.Application.Dtos.BookContent.Request;
using Sukun.Application.Dtos.BookContent.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IBookContentService
    {
        Task<Result<IEnumerable<BookContentResponseDto>>> GetBySectionAsync(Guid sectionId);
        Task<Result<BookContentResponseDto>> GetByIdAsync(Guid id);

        // Admin Operations
        Task<Result<BookContentResponseDto>> CreateAsync(Guid sectionId, BookContentCreateDto dto);
        Task<Result<BookContentResponseDto>> UpdateAsync(Guid id, BookContentUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}