using Sukun.Application.Dtos.AsmaulHusna.Responce;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IAsmaulHusnaService
    {
        Task<Result<IEnumerable<AsmaulHusnaResponseDto>>> GetAllAsync();
        Task<Result<AsmaulHusnaResponseDto>> GetByNumberAsync(int number);
        Task<Result<AsmaulHusnaResponseDto>> GetRandomAsync();
    }
}