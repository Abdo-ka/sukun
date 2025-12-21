using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Domin.Helping;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class NarrativeService : BaseService, INarrativeService
    {
        private readonly INarrativeRepository _narrativeRepository;
        private readonly INarrativeSectionRepository _sectionRepository;

        public NarrativeService(IUnitOfWork unitOfWork,
                                ILogger<NarrativeService> logger
                                )
            : base(unitOfWork, logger)
        {
            _narrativeRepository = unitOfWork.Narrative
                ?? throw new InvalidOperationException("NarrativeRepository not registered as INarrativeRepository");
            _sectionRepository = unitOfWork.NarrativeSection
                ?? throw new InvalidOperationException("NarrativeRepository not registered as INarrativeRepository");
            ;
        }

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetAllAsync()
        {
            var narratives = await _narrativeRepository.GetAllAsync();
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(narratives.Select(NarrativeMapper.ToListDto));
        }

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetRootAsync()
        {
            var narratives = await _narrativeRepository.GetRootNarrativesAsync();
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(narratives.Select(n => n.ToListDto()));
        }

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetByTypeAsync(ContentType type)
        {
            var narratives = await _narrativeRepository.GetByTypeAsync(type);
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(narratives.Select(n => n.ToListDto()));
        }

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetFeaturedAsync()
        {
            var narratives = await _narrativeRepository.GetFeaturedAsync();
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(narratives.Select(n => n.ToListDto()));
        }

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetChildrenAsync(Guid parentId)
        {
            var children = await _narrativeRepository.GetChildrenAsync(parentId);
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(children.Select(n => n.ToListDto()));
        }

        public async Task<Result<NarrativeResponseDto>> GetByIdWithFullDetailsAsync(Guid id)
        {
            var narrative = await _narrativeRepository.GetByIdWithFullDetailsAsync(id);
            if (narrative == null)
                return Result<NarrativeResponseDto>.NotFound("Narrative not found");

            await IncrementViewCountAsync(id); // زيادة المشاهدات عند القراءة الكاملة

            return Result<NarrativeResponseDto>.Success(narrative.ToResponseDtoWithChildren());
        }

        public async Task<Result> IncrementViewCountAsync(Guid id)
        {
            var narrative = await _narrativeRepository.GetByIdAsync(id);
            if (narrative == null) return Result.Failure("Narrative not found");

            narrative.ViewCount++;
            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }

        // ====================== Admin CRUD ======================

        public async Task<Result<NarrativeResponseDto>> CreateAsync(NarrativeCreateDto dto)
        {

            var narrative = await _narrativeRepository.AddAsync(dto.FromCreateDto());
            await _unitOfWork.CompleteAsync();

            var created = await _narrativeRepository.GetByIdWithFullDetailsAsync(narrative.Value.Id);
            return Result<NarrativeResponseDto>.Success(created!.ToResponseDtoWithChildren());
        }

        public async Task<Result<NarrativeResponseDto>> UpdateAsync(Guid id, NarrativeUpdateDto dto)
        {
            var narrative = await _narrativeRepository.GetByIdWithFullDetailsAsync(id);
            if (narrative == null)
                return Result<NarrativeResponseDto>.NotFound("Narrative not found");

            if (!string.IsNullOrEmpty(dto.Title)) narrative.Title = dto.Title;
            if (dto.TitleAr != null) narrative.TitleAr = dto.TitleAr;
            if (dto.Type.HasValue) narrative.Type = dto.Type.Value;
            if (dto.ShortDescription != null) narrative.ShortDescription = dto.ShortDescription;
            if (dto.ParentId.HasValue) narrative.ParentId = dto.ParentId;
            if (dto.CoverImageUrl != null) narrative.CoverImageUrl = dto.CoverImageUrl;
            if (dto.IsFeatured.HasValue) narrative.IsFeatured = dto.IsFeatured.Value;

            if (dto.Sections != null)
            {
                // تحسين: تحديث أو إضافة/حذف بناءً على ID (بدلاً من حذف الكل)
                var existingSections = narrative.Sections.ToList();
                foreach (var sectionDto in dto.Sections)
                {
                    if (sectionDto.Id.HasValue)
                    {
                        var existing = existingSections.FirstOrDefault(s => s.Id == sectionDto.Id.Value);
                        if (existing != null)
                        {
                            existing.Title = sectionDto.Title;
                            existing.Content = sectionDto.Content;
                            existing.DisplayOrder = sectionDto.DisplayOrder.Value;
                            existing.MediaUrl = sectionDto.MediaUrl;
                        }
                    }
                    else
                    {
                        narrative.Sections.Add(new NarrativeSection
                        {
                            Title = sectionDto.Title,
                            Content = sectionDto.Content,
                            DisplayOrder = sectionDto.DisplayOrder.Value,
                            MediaUrl = sectionDto.MediaUrl,
                            NarrativeId = id
                        });
                    }
                }
                // حذف الأقسام غير الموجودة في DTO
                foreach (var existing in existingSections)
                {
                    if (!dto.Sections.Any(s => s.Id == existing.Id))
                    {
                        narrative.Sections.Remove(existing);
                    }
                }
            }

            var updateResult = await _narrativeRepository.UpdateAsync(narrative);
            if (!updateResult.IsSuccess) return Result<NarrativeResponseDto>.Failure(updateResult.Message);
            await _unitOfWork.CompleteAsync();
            var updated = await _narrativeRepository.GetByIdWithFullDetailsAsync(id);
            return Result<NarrativeResponseDto>.Success(updated!.ToResponseDto());
        }
        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var narrative = await _narrativeRepository.GetByIdAsync(id);
            if (narrative == null)
                return Result.Failure("Narrative not found");

            narrative.IsDeleted = true;
            await _unitOfWork.CompleteAsync();

            return Result.Success();
        }
        public async Task<Result<NarrativeSectionResponseDto>> AddSectionAsync(Guid narrativeId, NarrativeSectionCreateDto dto)
        {
            // تحقق من وجود Narrative
            var narrativeExists = await _narrativeRepository.ExistsAsync(n => n.Id == narrativeId && !n.IsDeleted);
            if (!narrativeExists)
                return Result<NarrativeSectionResponseDto>.NotFound("Narrative not found");

            var section = dto.ToEntity(narrativeId);

            var addResult = await _sectionRepository.AddAsync(section);
            if (!addResult.IsSuccess)
                return Result<NarrativeSectionResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<NarrativeSectionResponseDto>.Success(section.ToResponseDto());
        }

        public async Task<Result<NarrativeSectionResponseDto>> UpdateSectionAsync(Guid sectionId, NarrativeSectionUpdateDto dto)
        {
            var section = await _sectionRepository.GetByIdAsync(sectionId);
            if (section == null)
                return Result<NarrativeSectionResponseDto>.NotFound("Section not found");

            section.UpdateFromDto(dto);

            var updateResult = await _sectionRepository.UpdateAsync(section);
            if (!updateResult.IsSuccess)
                return Result<NarrativeSectionResponseDto>.Failure(updateResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<NarrativeSectionResponseDto>.Success(section.ToResponseDto());
        }

        public async Task<Result> SoftDeleteSectionAsync(Guid sectionId)
        {
            var section = await _sectionRepository.GetByIdAsync(sectionId);
            if (section == null)
                return Result.NotFound("Section not found");

            var deleteResult = await _sectionRepository.DeleteAsync(section, softDelete: true);
            if (!deleteResult.IsSuccess)
                return Result.Failure(deleteResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result.Success();
        }

        public async Task<Result<NarrativeSectionResponseDto>> GetSectionByIdAsync(Guid sectionId)
        {
            var section = await _sectionRepository.GetByIdAsync(sectionId);
            if (section == null)
                return Result<NarrativeSectionResponseDto>.NotFound("Section not found");

            return Result<NarrativeSectionResponseDto>.Success(section.ToResponseDto());
        }
        public async Task<Result<PagedResponseDto<NarrativeListResponseDto>>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _narrativeRepository.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(n =>
                    n.Title.ToLower().Contains(term) ||
                    n.TitleAr != null && n.TitleAr.ToLower().Contains(term) ||
                    n.ShortDescription != null && n.ShortDescription.ToLower().Contains(term)
                );
            }

            query = query.OrderBy(n => n.Title);

            var dtoQuery = query.Select(n => n.ToListDto());

            var pagedResult = await dtoQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return Result<PagedResponseDto<NarrativeListResponseDto>>.Success(pagedResult);
        }
        private async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetChildrenByTagAsync(NarrativeTag tag)
        {
            var children = await _narrativeRepository.GetChildrenOfTaggedSectionAsync(tag);
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(
                children.Select(c => c.ToListDto()));
        }

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetLifeMainSectionsAsync()
            => await GetChildrenByTagAsync(NarrativeTag.ProphetLife);

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetBattlesAsync()
            => await GetChildrenByTagAsync(NarrativeTag.ProphetBattles);

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetWivesAsync()
            => await GetChildrenByTagAsync(NarrativeTag.ProphetWives);

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetCompanionsStoriesAsync()
            => await GetChildrenByTagAsync(NarrativeTag.Companions);

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetProphetsStoriesAsync()
            => await GetChildrenByTagAsync(NarrativeTag.ProphetsStories);


    }
}