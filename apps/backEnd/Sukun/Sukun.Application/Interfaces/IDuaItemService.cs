using Sukun.Application.Dtos.Dua.Request;
using Sukun.Application.Dtos.Dua.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IDuaItemService
    {
        Task<Result<IEnumerable<DuaItemResponseDto>>> GetByCategoryAsync(Guid categoryId);
        Task<Result<DuaItemResponseDto>> GetByIdAsync(Guid id);
        Task<Result<DuaItemResponseDto>> GetRandomAsync(); // دعاء عشوائي

        // Admin
        Task<Result<DuaItemResponseDto>> CreateAsync(DuaItemCreateDto dto);
        Task<Result<DuaItemResponseDto>> UpdateAsync(Guid id, DuaItemUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}