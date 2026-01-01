using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Application.Dtos.NarrativeSection.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Application.Interfaces
{
    public interface INarrativeService
    {
        Task<Result<NarrativeListResponseDto>> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetFeaturedAsync(int count = 10);
        Task<Result<NarrativeResponseDto>> GetByIdWithFullDetailsAsync(Guid id, ContentType? type = null, Guid? tagId = null, Guid? categoryId = null);
        Task<Result<IEnumerable<NarrativeResponseDto>>> GetAllWithFullContent(ContentType? type = null, Guid? tagId = null, Guid? categoryId = null);
        Task<Result<PagedResponseDto<NarrativeResponseDto>>> GetPagedAsync(PagedRequestDto request ,ContentType? type = null , Guid? tagId = null, Guid? categoryId = null);
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetAllAsync();
        Task<Result<IEnumerable<NarrativeListResponseDto>>> GetByTypeAsync(ContentType type);
        Task<Result<NarrativeResponseDto>> CreateAsync(NarrativeCreateDto dto);
        Task<Result<NarrativeResponseDto>> UpdateAsync(Guid id, NarrativeUpdateDto dto);
        Task<Result<bool>> SoftDeleteAsync(Guid id);
      
        Task<Result> IncrementViewCountAsync(Guid id);
        Task<Result<NarrativeSectionResponseDto>> AddSectionAsync(Guid narrativeId, NarrativeSectionCreateDto dto);
        Task<Result<NarrativeSectionResponseDto>> UpdateSectionAsync(Guid sectionId, NarrativeSectionUpdateDto dto);
        Task<Result> SoftDeleteSectionAsync(Guid sectionId);
        Task<Result<NarrativeSectionResponseDto>> GetSectionByIdAsync(Guid sectionId);

    }
}