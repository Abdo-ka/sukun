using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Application.Dtos.RemembranceCategory.Response;
using Sukun.Application.Dtos.RemembranceContent.Response;
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
    public class RemembranceService : IRemembranceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRemembranceRepository _remembranceRepository;
        private readonly IRemembranceCategoryRepository _categoryRepository;
        private readonly ISourceResolverService _sourceResolver;
        private readonly ILogger<RemembranceService> _logger;

        public RemembranceService(
            IUnitOfWork unitOfWork,
            IRemembranceRepository remembranceRepository,
            IRemembranceCategoryRepository categoryRepository,
            ISourceResolverService sourceResolver,
            ILogger<RemembranceService> logger)
        {
            _unitOfWork = unitOfWork;
            _remembranceRepository = remembranceRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _sourceResolver = sourceResolver;
        }

        public async Task<Result<IEnumerable<RemembranceResponseDto>>> GetByCategoryAsync(Guid categoryId)
        {
            var exists = await _categoryRepository.ExistsAsync(c => c.Id == categoryId);
            if (!exists)
                return Result<IEnumerable<RemembranceResponseDto>>.NotFound("Category not found");

            var remembrances = await _remembranceRepository.GetByCategoryWithFullDetailsAsync(categoryId);
           
            var allContents = remembrances
                .SelectMany(r => r.Contents)
                .Where(c => !c.IsDeleted)
                .ToList();

            var sourcePreviews = allContents.Any()
                ? await _sourceResolver.ResolveAsync(allContents)
                : new Dictionary<Guid, SourcePreviewDto>();

            var response = remembrances.Select(r => r.ToResponseDto(sourcePreviews));
            return Result<IEnumerable<RemembranceResponseDto>>.Success(response);
        }
        public async Task<Result<RemembranceResponseDto>> GetRandomAsync()
        {
            var remembrance = await _remembranceRepository.GetRandomWithFullDetailsAsync();
            if (remembrance == null)
                return Result<RemembranceResponseDto>.NotFound("No remembrance found");
            var contents = remembrance.Contents.Where(c => !c.IsDeleted).ToList();

            var sourcePreviews = contents.Any()
                ? await _sourceResolver.ResolveAsync(contents)
                : new Dictionary<Guid, SourcePreviewDto>();

            var remembranceDto = remembrance.ToResponseDto(sourcePreviews);
            return Result<RemembranceResponseDto>.Success(remembranceDto);
        }
        public async Task<Result<RemembranceResponseDto>> CreateAsync(RemembranceCreateDto dto)
        {
            var remembrance = dto.ToEntity();

            if (dto.CategoryIds.Any())
            {
                remembrance.RemembranceCategoryLinks = dto.CategoryIds
                    .Select(catId => new RemembranceCategoryLinks
                    {
                        RemembranceId = remembrance.Id,
                        RemembranceCategoryId = catId
                    })
                    .ToList();
            }

            foreach (var content in remembrance.Contents)
            {
                content.RemembranceId = remembrance.Id;
            }

            var externalContents = remembrance.Contents
                .Where(c => c.SourceType != SourceType.Custom && c.SourceId.HasValue)
                .ToList();

            if (externalContents.Any())
            {
                var resolvedPreviews = await _sourceResolver.ResolveAsync(externalContents);

                var requestedIds = externalContents.Select(c => c.SourceId!.Value).ToHashSet();
                var foundIds = resolvedPreviews.Keys.ToHashSet();

                var missingIds = requestedIds.Except(foundIds).ToList();

                if (missingIds.Any())
                {
                    return Result<RemembranceResponseDto>.BadRequest(
                        $"The following SourceIds do not exist in the database: {string.Join(", ", missingIds)}");
                }
            }

            var addResult = await _remembranceRepository.AddAsync(remembrance);
            if (!addResult.IsSuccess)
                return Result<RemembranceResponseDto>.Failure(addResult.Message ?? "Failed to add remembrance.");

            await _unitOfWork.CompleteAsync();

            var created = await GetByIdWithFullContentAsync(remembrance.Id);
            if (!created.IsSuccess)
                return Result<RemembranceResponseDto>.Failure("Failed to retrieve the created remembrance.");

            return Result<RemembranceResponseDto>.Success(created.Value);
        }
        public async Task<Result<RemembranceResponseDto>> UpdateAsync(Guid id, RemembranceUpdateDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var remembrance = await _remembranceRepository.GetByIdWithFullDetailsAsync(id);
                if (remembrance == null || remembrance.IsDeleted)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<RemembranceResponseDto>.NotFound("Remembrance not found");
                }

                if (!string.IsNullOrWhiteSpace(dto.Title))
                    remembrance.Title = dto.Title.Trim();

                remembrance.RecommendedCount = dto.RecommendedCount;

                if (dto.Benefits != null)
                    remembrance.Benefits = dto.Benefits;

                remembrance.UpdatedAt = DateTime.UtcNow;

                if (dto.CategoryIds != null)
                {
                    var desiredCategoryIds = dto.CategoryIds.Distinct().ToList();
                    var currentLinks = remembrance.RemembranceCategoryLinks.ToList();
                    var currentCategoryIds = currentLinks.Select(l => l.RemembranceCategoryId).ToList();

                    var linksToRemove = currentLinks
                        .Where(l => !desiredCategoryIds.Contains(l.RemembranceCategoryId))
                        .ToList();

                    var linksToAdd = desiredCategoryIds
                        .Except(currentCategoryIds)
                        .Select(catId => new RemembranceCategoryLinks
                        {
                            RemembranceId = id,
                            RemembranceCategoryId = catId
                        })
                        .ToList();

                    if (linksToRemove.Any())
                        await _unitOfWork.Repository<RemembranceCategoryLinks>().DeleteRangeAsync(linksToRemove);

                    if (linksToAdd.Any())
                        await _unitOfWork.Repository<RemembranceCategoryLinks>().AddRangeAsync(linksToAdd);
                }

                if (dto.Contents != null && dto.Contents.Any())
                {
                    var sentContentIds = dto.Contents
                        .Where(c => c.Id.HasValue)
                        .Select(c => c.Id.Value)
                        .ToList();

                    var currentContentIds = remembrance.Contents
                        .Where(c => !c.IsDeleted)
                        .Select(c => c.Id)
                        .ToHashSet();

                    var invalidIds = sentContentIds.Where(sentId => !currentContentIds.Contains(sentId)).ToList();
                    if (invalidIds.Any())
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        return Result<RemembranceResponseDto>.BadRequest(
                            $"The following content IDs do not exist or do not belong to this remembrance: {string.Join(", ", invalidIds)}");
                    }
               
                    var newContents = dto.Contents
                        .Where(c => !c.Id.HasValue)
                        .Select(c => new RemembranceContent
                        {
                            Id = Guid.NewGuid(),
                            RemembranceId = id,
                            SourceType = c.SourceType,
                            SourceId = c.SourceId,
                            CustomContent = c.CustomContent,
                            CreateAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        })
                        .ToList();

                    if (newContents.Any())
                    {
                        await _unitOfWork.RemembranceContents.AddRangeAsync(newContents);
                        foreach (var newContent in newContents)
                        {
                            remembrance.Contents.Add(newContent);
                        }
                    }
                   
                    foreach (var contentDto in dto.Contents.Where(c => c.Id.HasValue))
                    {
                        var existingContent = remembrance.Contents
                            .FirstOrDefault(c => c.Id == contentDto.Id!.Value && !c.IsDeleted);
              
                        if (existingContent != null)
                        {
                            existingContent.SourceType = contentDto.SourceType;
                            existingContent.SourceId = contentDto.SourceId;
                            existingContent.CustomContent = contentDto.CustomContent;
                            existingContent.UpdatedAt = DateTime.UtcNow;

                            await _unitOfWork.RemembranceContents.UpdateAsync(existingContent);
                        }
                    }

                    var sentIdsHash = sentContentIds.ToHashSet();
                    var contentsToSoftDelete = remembrance.Contents
                        .Where(c => currentContentIds.Contains(c.Id) && !c.IsDeleted && !sentIdsHash.Contains(c.Id))
                        .ToList();

                    if (contentsToSoftDelete.Any())
                    {
                        await _unitOfWork.RemembranceContents.DeleteRangeAsync(contentsToSoftDelete);
                    }
                }
                var externalContents = remembrance.Contents
                .Where(c => c.SourceType != SourceType.Custom && c.SourceId.HasValue)
                .ToList();

                if (externalContents.Any())
                {
                    var resolvedPreviews = await _sourceResolver.ResolveAsync(externalContents);

                    var requestedIds = externalContents.Select(c => c.SourceId!.Value).ToHashSet();
                    var foundIds = resolvedPreviews.Keys.ToHashSet();

                    var missingIds = requestedIds.Except(foundIds).ToList();

                    if (missingIds.Any())
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        return Result<RemembranceResponseDto>.BadRequest(
                            $"The following SourceIds do not exist in the database: {string.Join(", ", missingIds)}");
                    }
                }
                var updatedResult = await _unitOfWork.Remembrances.UpdateAsync(remembrance);
                if (!updatedResult.IsSuccess)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<RemembranceResponseDto>.Failure("Failed to retrieve updated remembrance.");
                }
                await _unitOfWork.CommitTransactionAsync();

                var updatedResponseResult = await GetByIdWithFullContentAsync(id);
                if (!updatedResponseResult.IsSuccess)
                    return Result<RemembranceResponseDto>.Failure("Failed to retrieve updated remembrance.");

                return Result<RemembranceResponseDto>.Success(updatedResponseResult.Value);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating remembrance {Id}", id);
                return Result<RemembranceResponseDto>.Failure("An error occurred while updating the remembrance");
            }
        }
        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var remembrance = await _remembranceRepository.GetByIdAsync(id);
            if (remembrance == null)
                return Result.NotFound("Remembrance not found");

            remembrance.IsDeleted = true;
            remembrance.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
        public async Task<Result<RemembranceResponseDto>> GetByIdWithFullContentAsync(Guid id)
        {
            var remembrance = await _remembranceRepository.GetByIdWithFullDetailsAsync(id);
            if (remembrance == null)
                return Result<RemembranceResponseDto>.NotFound("Invalid RemembranceId");


            var sourcePreviews = remembrance.Contents.Any()
                ? await _sourceResolver.ResolveAsync(remembrance.Contents)
                : new Dictionary<Guid, SourcePreviewDto>();

            var remembranceDto = remembrance.ToResponseDto(sourcePreviews);

            return Result<RemembranceResponseDto>.Success(remembranceDto);
        }
        public async Task<Result<List<RemembranceResponseDto>>> GetAllWithFullContentAsync()
        {
            var remembrances = await _remembranceRepository.GetAllWithFullDetailsAsync();
            if (!remembrances.Any())
                return Result<List<RemembranceResponseDto>>.Success(new List<RemembranceResponseDto>());

            var allContents = remembrances
                .SelectMany(r => r.Contents)
                .Where(c => !c.IsDeleted)
                .ToList();

            var sourcePreviews = allContents.Any()
                ? await _sourceResolver.ResolveAsync(allContents)
                : new Dictionary<Guid, SourcePreviewDto>();

            var response = remembrances.ToResponseDtoList(sourcePreviews);

            return Result<List<RemembranceResponseDto>>.Success(response);
        }
        public async Task<Result<PagedResponseDto<RemembranceResponseDto>>> GetPagedWithFullContentAsync(PagedRequestDto request,  Guid? categoryId = null)
        {
            var (items, totalCount) = await _remembranceRepository.GetPagedWithFullDetailsAsync(request, categoryId);
           
            if (!items.Any())
            {
                var emptyPaged = new PagedResponseDto<RemembranceResponseDto>
                {
                    Items = new List<RemembranceResponseDto>(),
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = 0,
                    TotalPages = 0,
                    HasPreviousPage = false,
                    HasNextPage = false
                };
                return Result<PagedResponseDto<RemembranceResponseDto>>.Success(emptyPaged);
            }

            var pageContents = items.SelectMany(r => r.Contents)
                                    .Where(c => !c.IsDeleted).ToList();

            var sourcePreviews = pageContents.Any()
                ? await _sourceResolver.ResolveAsync(pageContents)
                : new Dictionary<Guid, SourcePreviewDto>();

            var responseItems = items.ToResponseDtoList(sourcePreviews);

            var pagedResponse = new PagedResponseDto<RemembranceResponseDto>
            {
                Items = responseItems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                HasPreviousPage = request.PageNumber > 1,
                HasNextPage = request.PageNumber < (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };

            return Result<PagedResponseDto<RemembranceResponseDto>>.Success(pagedResponse);
        }
    }
}