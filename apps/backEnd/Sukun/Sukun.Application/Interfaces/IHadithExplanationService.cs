using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Domin.Common;

namespace Sukun.Application.Implemantation
{
    public interface IHadithExplanationService
    {
        Task<Result<IEnumerable<HadithExplanationResponseDto>>> GetByHadithAsync(Guid hadithId);
        Task<Result<HadithExplanationResponseDto>> GetByIdAsync(Guid id);

        // Admin
        Task<Result<HadithExplanationResponseDto>> CreateAsync(Guid hadithId, HadithExplanationCreateDto dto);
        Task<Result<HadithExplanationResponseDto>> UpdateAsync(Guid id, HadithExplanationUpdateDto dto);
        Task<Result> DeleteAsync(Guid id); // Soft أو Hard حسب الحاجة
    }
}