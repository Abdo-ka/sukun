using Sukun.Application.Dtos.Tasbih.Request;
using Sukun.Application.Dtos.Tasbih.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface ITasbihService
    {
        Task<Result<IEnumerable<TasbihResponseDto>>> GetAllAsync();
        Task<Result<TasbihResponseDto>> GetByIdAsync(Guid id);
        Task<Result<TasbihResponseDto>> GetRandomAsync();

        // Admin
        Task<Result<TasbihResponseDto>> CreateAsync(TasbihCreateDto dto);
        Task<Result<TasbihResponseDto>> UpdateAsync(Guid id, TasbihUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}