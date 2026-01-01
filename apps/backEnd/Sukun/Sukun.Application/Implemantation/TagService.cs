using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Tag.Request;
using Sukun.Application.Dtos.Tag.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Helping;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITagRepository _tagRepo;
        private readonly ILogger<TagService> _logger;

        public TagService(IUnitOfWork unitOfWork, ILogger<TagService> logger)
        {
            _unitOfWork = unitOfWork;
            _tagRepo = unitOfWork.Tags;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<TagResponseDto>>> GetAllAsync()
        {
            try
            {
                var tags = await _tagRepo.GetAllAsync();
                var dtos = tags.Select(t => t.ToResponseDto());
                return Result<IEnumerable<TagResponseDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all tags");
                return Result<IEnumerable<TagResponseDto>>.BadRequest("An error occurred");
            }
        }

        public async Task<Result<PagedResponseDto<TagResponseDto>>> GetPagedAsync(PagedRequestDto request)
        {
            try
            {
                if (request.PageNumber < 1) request.PageNumber = 1;
                if (request.PageSize < 1 || request.PageSize > 100) request.PageSize = 20;
              
                var query =  _tagRepo.AsQueryableNoTracking();
                
                if (!string.IsNullOrEmpty(request.SearchTerm))
                    query = query.Where(t => t.Name.ToLower().Contains(request.SearchTerm.Trim().ToLowerInvariant()) ||
                                    (t.NameAr != null && t.NameAr.ToLower().Contains(request.SearchTerm.Trim().ToLowerInvariant())));

                query = query.OrderBy(t => t.Name);

                var paged = await query.Select(x=>x.ToResponseDto()).ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return Result<PagedResponseDto<TagResponseDto>>.Success(paged);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paged tags");
                return Result<PagedResponseDto<TagResponseDto>>.BadRequest("An error occurred");
            }
        }

        public async Task<Result<TagResponseDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var tag = await _tagRepo.GetByIdAsync(id);
                if (tag == null)
                    return Result<TagResponseDto>.NotFound("Tag not found");

                return Result<TagResponseDto>.Success(tag.ToResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tag {Id}", id);
                return Result<TagResponseDto>.BadRequest("An error occurred");
            }
        }

        public async Task<Result<TagResponseDto>> CreateAsync(TagCreateDto dto)
        {
            try
            {
                // التحقق من عدم التكرار (حتى مع اختلاف حالة الأحرف)
                var normalized = dto.Name.Trim().ToLowerInvariant();
                var existing = await _tagRepo.FirstOrDefaultAsync(t =>
                    t.Name.ToLower() == normalized);

                if (existing != null)
                    return Result<TagResponseDto>.BadRequest("Tag with similar name already exists");

                var tag = dto.ToEntity();
                var result = await _tagRepo.AddAsync(tag);
                if (!result.IsSuccess)
                    return Result<TagResponseDto>.Failure(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<TagResponseDto>.Success(tag.ToResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating tag");
                return Result<TagResponseDto>.BadRequest("An error occurred while creating the tag");
            }
        }

        public async Task<Result<TagResponseDto>> UpdateAsync(Guid id, TagUpdateDto dto)
        {
            try
            {
                var tag = await _tagRepo.GetByIdAsync(id);
                if (tag == null)
                    return Result<TagResponseDto>.NotFound("Tag not found");

                if (!string.IsNullOrEmpty(dto.Name))
                {
                    var normalized = dto.Name.Trim().ToLowerInvariant();
                    var duplicate = await _tagRepo.FirstOrDefaultAsync(t =>
                        t.Id != id && t.Name.ToLower() == normalized);

                    if (duplicate != null)
                        return Result<TagResponseDto>.BadRequest("Another tag with similar name already exists");

                    tag.Name = dto.Name.Trim();
                }

                if (dto.NameAr != null)
                    tag.NameAr = dto.NameAr.Trim();

                tag.UpdatedAt = DateTime.UtcNow;

                await _tagRepo.UpdateAsync(tag);
                await _unitOfWork.CompleteAsync();

                return Result<TagResponseDto>.Success(tag.ToResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tag {Id}", id);
                return Result<TagResponseDto>.BadRequest("An error occurred");
            }
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            try
            {
                var tag = await _tagRepo.GetByIdAsync(id);
                if (tag == null)
                    return Result.NotFound("Tag not found");

                await _tagRepo.DeleteAsync(tag);
                await _unitOfWork.CompleteAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tag {Id}", id);
                return Result.BadRequest("An error occurred");
            }
        }

        public async Task<Result<IEnumerable<TagResponseDto>>> SearchAsync(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return Result<IEnumerable<TagResponseDto>>.Success(Enumerable.Empty<TagResponseDto>());

                var normalized = query.Trim().ToLowerInvariant();

                var tags = await _tagRepo.AsQueryableNoTracking()
                    .Where(t => t.Name.ToLower().Contains(normalized) ||
                                (t.NameAr != null && t.NameAr.ToLower().Contains(normalized)))
                    .OrderBy(t => t.Name)
                    .Take(10)
                    .ToListAsync();

                var dtos = tags.Select(t => t.ToResponseDto());
                return Result<IEnumerable<TagResponseDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching tags with query {Query}", query);
                return Result<IEnumerable<TagResponseDto>>.BadRequest("An error occurred");
            }
        }
    }
}