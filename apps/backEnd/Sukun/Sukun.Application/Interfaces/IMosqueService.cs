using Sukun.Application.Dtos.Mosque.Request;
using Sukun.Application.Dtos.Mosque.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface IMosqueService
    {
        Task<Result<IEnumerable<MosqueResponseDto>>> GetByCityAsync(Guid cityId);
        Task<Result<IEnumerable<MosqueResponseDto>>> GetNearbyAsync(double latitude, double longitude, double radiusInKm = 10);
        Task<Result<IEnumerable<MosqueResponseDto>>> GetJummahMosquesAsync(Guid cityId);
        Task<Result<MosqueResponseDto>> GetByIdAsync(Guid id);

        // Admin
        Task<Result<MosqueResponseDto>> CreateAsync(MosqueCreateDto dto);
        Task<Result<MosqueResponseDto>> UpdateAsync(Guid id, MosqueUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
    }
}