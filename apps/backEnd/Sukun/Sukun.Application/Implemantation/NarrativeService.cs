using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Narrative.Request;
using Sukun.Application.Dtos.Narrative.Response;
using Sukun.Application.Dtos.NarrativeSection.Request;
using Sukun.Application.Dtos.NarrativeSection.Response;
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

    

        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetFeaturedAsync(int count = 10)
        {
            try
            {
                var narratives = await _narrativeRepository.GetFeaturedAsync(count);
                var dtos = narratives.Select(n => n.ToListDto());
                return Result<IEnumerable<NarrativeListResponseDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving featured narratives");
                return Result<IEnumerable<NarrativeListResponseDto>>.BadRequest("An error occurred");
            }
        }

        public async Task<Result<PagedResponseDto<NarrativeResponseDto>>> GetPagedAsync(PagedRequestDto request , ContentType? type = null, Guid? tagId = null , Guid? categoryId = null)
        {
            if (request.PageNumber < 1) request.PageNumber = 1;
            if (request.PageSize < 1 || request.PageSize > 100) request.PageSize = 20;
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

            query = query.Include(n=>n.Sections)
                         .Include(n => n.NarrativeTags)
                            .ThenInclude(n=>n.Tag)
                         .Include(n => n.NarrativeCategories)
                            .ThenInclude(nc=>nc.Category);

            if (type.HasValue)
                query = query.Where(n => n.Type == type.Value);

            if (tagId.HasValue)
                query = query.Where(n => n.NarrativeTags.Any(nt=>nt.TagId == tagId.Value));

            if (categoryId.HasValue)
                query = query.Where(n => n.NarrativeCategories.Any(nc => nc.CategoryId == categoryId.Value));

            var dtoQuery = query.Select(n => n.ToResponseDto());

            var pagedResult = await dtoQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return Result<PagedResponseDto<NarrativeResponseDto>>.Success(pagedResult);
        }
        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetAllAsync()
        {
            var narratives = await _narrativeRepository.GetAllAsync();
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(narratives.Select(NarrativeMapper.ToListDto));
        }
        public async Task<Result<IEnumerable<NarrativeListResponseDto>>> GetByTypeAsync(ContentType type)
        {
            var narratives = await _narrativeRepository.GetByTypeAsync(type);
            return Result<IEnumerable<NarrativeListResponseDto>>.Success(narratives.Select(n => n.ToListDto()));
        }

        public async Task<Result<IEnumerable<NarrativeResponseDto>>> GetAllWithFullContent(ContentType? type = null, Guid? tagId = null, Guid? categoryId = null)
        {
            var narratives = await _narrativeRepository.GetAllWithDetailsAsync(type, tagId , categoryId);
            return Result<IEnumerable<NarrativeResponseDto>>.Success(narratives.Select(n => n.ToResponseDto()));
        }
        public async Task<Result<NarrativeResponseDto>> GetByIdWithFullDetailsAsync(Guid id, ContentType? type = null, Guid? tagId = null, Guid? categoryId = null)
        {
            var narrative = await _narrativeRepository.GetByIdWithDetailsAsync(id, type, tagId, categoryId);
            if (narrative == null)
                return Result<NarrativeResponseDto>.NotFound("Narrative not found");

            await IncrementViewCountAsync(id);

            return Result<NarrativeResponseDto>.Success(narrative.ToResponseDto());
        }
        public async Task<Result<NarrativeListResponseDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var narrative = await _narrativeRepository.GetByIdAsync(id);
                if (narrative == null)
                    return Result<NarrativeListResponseDto>.NotFound("Narrative not found");

                await IncrementViewCountAsync(id);

                var dto = narrative.ToListDto();
                return Result<NarrativeListResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving narrative {Id}", id);
                return Result<NarrativeListResponseDto>.BadRequest("An error occurred while retrieving the narrative");
            }
        }
        public async Task<Result> IncrementViewCountAsync(Guid id)
        {
            var narrative = await _narrativeRepository.GetByIdAsync(id);
            if (narrative == null) return Result.Failure("Narrative not found");

            narrative.ViewCount++;
            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }

        public async Task<Result<NarrativeResponseDto>> CreateAsync(NarrativeCreateDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var narrative = dto.FromCreateDto();


                if (dto.TagIds.Any())
                {
                    narrative.NarrativeTags = dto.TagIds.Select(tagId => new NarrativeTags
                    {
                        TagId = tagId
                    }).ToList();
                }

                if (dto.CategoryIds.Any())
                {
                    narrative.NarrativeCategories = dto.CategoryIds.Select(catId => new NarrativeCategory
                    {
                        CategoryId = catId,
                        DisplayOrder = 0
                    }).ToList();

                }
                var addResult = await _narrativeRepository.AddAsync(narrative);
                if (!addResult.IsSuccess)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<NarrativeResponseDto>.Failure(addResult.Message);
                }

                await _unitOfWork.CommitTransactionAsync();

                var created = await _narrativeRepository.GetByIdWithDetailsAsync(narrative.Id);
                return Result<NarrativeResponseDto>.Success(created!.ToResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating narrative");
                return Result<NarrativeResponseDto>.BadRequest("An error occurred while creating the narrative");
            }
        }

        public async Task<Result<NarrativeResponseDto>> UpdateAsync(Guid id, NarrativeUpdateDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var narrative = await _narrativeRepository.GetByIdWithDetailsAsync(id);
                if (narrative == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<NarrativeResponseDto>.NotFound("Narrative not found");
                }

               
                if (dto.TagIds != null)
                {
                    var currentTags = narrative.NarrativeTags.ToList();

                    var desiredTagIds = dto.TagIds.Distinct().ToList();
                    var currentTagIds = currentTags.Select(nt => nt.TagId).ToList();
                    var toAdd = desiredTagIds.Except(currentTagIds).Select(tagId => new NarrativeTags
                    {
                        NarrativeId = id,
                        TagId = tagId
                    }).ToList();

                    var toRemoveTags = currentTags
                                        .Where(nt => !desiredTagIds.Contains(nt.TagId))
                                        .ToList();

                    if (toAdd.Any())
                        await _unitOfWork.NarrativeTags.AddRangeAsync(toAdd);

                    if (toRemoveTags.Any())
                         await _unitOfWork.NarrativeTags.DeleteRangeAsync(toRemoveTags);
                }

                if (dto.CategoryIds != null)
                {

                    var currentCategories = narrative.NarrativeCategories.ToList();

                    var currentCategoryIds = currentCategories.Select(nc => nc.CategoryId).ToList();
                    var desiredCategoryIds = dto.CategoryIds.Distinct().ToList();

                    var toAddCats = desiredCategoryIds.Except(currentCategoryIds).Select(catId => new NarrativeCategory
                    {
                        NarrativeId = id,
                        CategoryId = catId,
                        DisplayOrder = 0
                    }).ToList();

                    var toRemoveCats = currentCategories
                                      .Where(nc => !desiredCategoryIds.Contains(nc.CategoryId))
                                      .ToList();

                    if (toAddCats.Any())
                        await _unitOfWork.NarrativeCategories.AddRangeAsync(toAddCats);

                    if (toRemoveCats.Any())
                        await _unitOfWork.NarrativeCategories.DeleteRangeAsync(toRemoveCats);
                } 

                #region
                var sentSectionIds = dto.Sections.Where(s => s.Id.HasValue).Select(s => s.Id.Value).ToList();
                if (sentSectionIds.Distinct().Count() != sentSectionIds.Count)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<NarrativeResponseDto>.BadRequest("Duplicate section IDs are not allowed");
                }

                // 2. فحص أن كل Id مرسل موجود فعليًا في DB وينتمي للـ Narrative
                var currentSectionIds = narrative.Sections.Select(s => s.Id).ToHashSet();
                var invalidIds = sentSectionIds.Where(sentId => !currentSectionIds.Contains(sentId)).ToList();

                if (invalidIds.Any())
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<NarrativeResponseDto>.BadRequest(
                        $"The following section IDs do not exist or do not belong to this narrative: {string.Join(", ", invalidIds)}");
                }

                // 3. إضافة الأقسام الجديدة (بدون Id)
                var newSections = dto.Sections
                    .Where(s => !s.Id.HasValue)
                    .Select(s => new NarrativeSection
                    {
                        Id = Guid.NewGuid(),
                        NarrativeId = id,
                        Title = s.Title ?? "New Section",
                        Content = s.Content ?? string.Empty,
                        DisplayOrder = s.DisplayOrder ?? narrative.Sections.Count + 1,
                        CreateAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    })
                    .ToList();

                if (newSections.Any())
                {
                    foreach (var section in newSections)
                    {
                        await _unitOfWork.NarrativeSection.AddAsync(section);
                    }
                }

                // 4. تعديل الأقسام الموجودة (مع Id)
                foreach (var sectionDto in dto.Sections.Where(s => s.Id.HasValue))
                {
                    var sectionId = sectionDto.Id!.Value;

                    var existingSection = await _unitOfWork.NarrativeSection.AsQueryableNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == sectionId && s.NarrativeId == id);

                    if (existingSection != null)
                    {
                        if (!string.IsNullOrEmpty(sectionDto.Title)) existingSection.Title = sectionDto.Title;
                        if (sectionDto.Content != null) existingSection.Content = sectionDto.Content;
                        if (sectionDto.DisplayOrder.HasValue) existingSection.DisplayOrder = sectionDto.DisplayOrder.Value;

                        existingSection.UpdatedAt = DateTime.UtcNow;

                        await _unitOfWork.NarrativeSection.UpdateAsync(existingSection);
                    }
                }

                // 5. حذف الأقسام غير المرسلة
                var sentIdsHash = sentSectionIds.ToHashSet();
                var sectionsToDelete = narrative.Sections
                    .Where(s => currentSectionIds.Contains(s.Id) && !sentIdsHash.Contains(s.Id))
                    .ToList();

                if (sectionsToDelete.Any())
                {
                    await _unitOfWork.NarrativeSection.DeleteRangeAsync(sectionsToDelete);
                    foreach (var section in sectionsToDelete)
                    {
                        if (sectionsToDelete.Contains(section))
                            narrative.Sections.Remove(section);
                    }
                }
                #endregion

                if (!string.IsNullOrEmpty(dto.Title)) narrative.Title = dto.Title;
                if (dto.TitleAr != null) narrative.TitleAr = dto.TitleAr;
                if (dto.ShortDescription != null) narrative.ShortDescription = dto.ShortDescription;
                if (dto.IsFeatured) narrative.IsFeatured = dto.IsFeatured;
                narrative.Type = dto.Type;

                narrative.UpdatedAt = DateTime.UtcNow;
                var updateResult=  await _narrativeRepository.UpdateAsync(narrative);
                if(!updateResult.IsSuccess)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    _logger.LogError("Error updating narrative {Id}", id);
                    return Result<NarrativeResponseDto>.BadRequest("An error occurred while updating the narrative");
                }
                await _unitOfWork.CommitTransactionAsync();

                var updated = await _narrativeRepository.GetByIdWithDetailsAsync(id);
                return Result<NarrativeResponseDto>.Success(updated!.ToResponseDto());
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating narrative {Id}", id);
                return Result<NarrativeResponseDto>.BadRequest("An error occurred while updating the narrative");
            }
        }

        public async Task<Result<bool>> SoftDeleteAsync(Guid id)
        {
            try
            {
                var narrative = await _narrativeRepository.GetByIdAsync(id);
                if (narrative == null)
                    return Result<bool>.NotFound("Narrative not found");

                var deleteResult = await _narrativeRepository.DeleteAsync(narrative);
                if (!deleteResult.IsSuccess)
                    return deleteResult;

                await _unitOfWork.CompleteAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting narrative {Id}", id);
                return Result<bool>.BadRequest("An error occurred while deleting the narrative");
            }
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



    }
}