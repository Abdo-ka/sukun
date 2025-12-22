using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;

namespace Sukun.Application.Interfaces
{
    public interface INarrativeService
    {
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetAllAsync();
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetRootAsync(); // الرئيسية فقط
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetByTypeAsync(ContentType type);
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetFeaturedAsync();
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetChildrenAsync(Guid parentId);
        Task<Result<NarrativeResponseDto>> GetByIdWithFullDetailsAsync(Guid id);
        Task<Result> IncrementViewCountAsync(Guid id);
        Task<Result<NarrativeResponseDto>> CreateAsync(NarrativeCreateDto dto);
        Task<Result<NarrativeResponseDto>> UpdateAsync(Guid id, NarrativeUpdateDto dto);
        Task<Result> SoftDeleteAsync(Guid id);
        Task<Result<NarrativeSectionResponseDto>> AddSectionAsync(Guid narrativeId, NarrativeSectionCreateDto dto);
        Task<Result<NarrativeSectionResponseDto>> UpdateSectionAsync(Guid sectionId, NarrativeSectionUpdateDto dto);
        Task<Result> SoftDeleteSectionAsync(Guid sectionId);
        Task<Result<NarrativeSectionResponseDto>> GetSectionByIdAsync(Guid sectionId);
        Task<Result<PagedResponseDto<NarrativeListResponseDto>>> GetPagedAsync(PagedRequestDto request);
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetLifeMainSectionsAsync();

        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetBattlesAsync();

        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetWivesAsync();

        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetCompanionsStoriesAsync();

        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetsStoriesAsync();

    }

}