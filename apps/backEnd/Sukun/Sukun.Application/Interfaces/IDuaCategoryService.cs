using Sukun.Application.Dtos.Dua.Response;
using Sukun.Application.Dtos.DuaCategory.Request;
using Sukun.Application.Dtos.DuaCategory.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IDuaCategoryService
    {
        Task<Result<IEnumerable<DuaCategoryResponseDto>>> GetAllAsync();
        Task<Result<DuaCategoryWithDuasDto>> GetByIdWithDuasAsync(Guid id);

        // Admin
        Task<Result<DuaCategoryResponseDto>> CreateAsync(DuaCategoryCreateDto dto);
        Task<Result<DuaCategoryResponseDto>> UpdateAsync(Guid id, DuaCategoryUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}