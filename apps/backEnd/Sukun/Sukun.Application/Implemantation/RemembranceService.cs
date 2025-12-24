using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Remembrance.Request;
using Sukun.Application.Dtos.Remembrance.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class RemembranceService : IRemembranceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRemembranceRepository _remembranceRepository;
        private readonly IRemembranceCategoryRepository _categoryRepository;
        private readonly ILogger<RemembranceService> _logger;

        public RemembranceService(
            IUnitOfWork unitOfWork,
            IRemembranceRepository remembranceRepository,
            IRemembranceCategoryRepository categoryRepository,
            ILogger<RemembranceService> logger)
        {
            _unitOfWork = unitOfWork;
            _remembranceRepository = remembranceRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<RemembranceResponseDto>>> GetByCategoryAsync(Guid categoryId)
        {
            var exists = await _categoryRepository.ExistsAsync(c => c.Id == categoryId);
            if (!exists)
                return Result<IEnumerable<RemembranceResponseDto>>.NotFound("Category not found");

            var remembrances = await _remembranceRepository.GetByCategoryAsync(categoryId);
            return Result<IEnumerable<RemembranceResponseDto>>.Success(remembrances.Select(r => r.ToResponseDto()));
        }

        public async Task<Result<RemembranceResponseDto>> GetByIdAsync(Guid id)
        {
            var remembrance = await _remembranceRepository.GetByIdWithDetailsAsync(id);
            if (remembrance == null)
                return Result<RemembranceResponseDto>.NotFound("Remembrance not found");

            return Result<RemembranceResponseDto>.Success(remembrance.ToResponseDto());
        }

        public async Task<Result<RemembranceResponseDto>> GetRandomAsync()
        {
            var remembrance = await _remembranceRepository.GetRandomAsync();
            if (remembrance == null)
                return Result<RemembranceResponseDto>.NotFound("No remembrance found");

            return Result<RemembranceResponseDto>.Success(remembrance.ToResponseDto());
        }

        // ====================== Admin ======================

        public async Task<Result<RemembranceResponseDto>> CreateAsync(RemembranceCreateDto dto)
        {
            // التحقق من الفئات
            if (dto.CategoryIds.Any())
            {
                var categories = await _categoryRepository.FindAsync(c => dto.CategoryIds.Contains(c.Id));
                if (categories.Count() != dto.CategoryIds.Count)
                    return Result<RemembranceResponseDto>.BadRequest("One or more CategoryIds are invalid");
            }

            var remembrance = dto.ToEntity();

            // ربط الفئات
            if (dto.CategoryIds.Any())
            {
                var categories = await _categoryRepository.FindAsync(c => dto.CategoryIds.Contains(c.Id));
                remembrance.RemembranceCategoryLinks = categories.Select(x=> new RemembranceCategoryLinks { RemembranceCategoryId = x.Id}).ToList();
            }

            // ربط المحتوى
            if (dto.Contents != null)
            {
                remembrance.Contents = dto.Contents.Select(c => new RemembranceContent
                {
                    Id = Guid.NewGuid(),
                    SourceType = c.SourceType,
                    SourceId = c.SourceId,
                    CustomContent = c.CustomContent,
                    RemembranceId = remembrance.Id
                }).ToList();
            }

            var addResult = await _remembranceRepository.AddAsync(remembrance);
            if (!addResult.IsSuccess)
                return Result<RemembranceResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            var created = await _remembranceRepository.GetByIdWithDetailsAsync(remembrance.Id);
            return Result<RemembranceResponseDto>.Success(created!.ToResponseDto());
        }

        public async Task<Result<RemembranceResponseDto>> UpdateAsync(Guid id, RemembranceUpdateDto dto)
        {
            var remembrance = await _remembranceRepository.GetByIdWithDetailsAsync(id);
            if (remembrance == null)
                return Result<RemembranceResponseDto>.NotFound("Remembrance not found");

            if (!string.IsNullOrEmpty(dto.Title)) remembrance.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Text)) remembrance.Text = dto.Text;
            if (dto.RecommendedCount > 0) remembrance.RecommendedCount = dto.RecommendedCount;
            if (dto.Benefits != null) remembrance.Benefits = dto.Benefits;
            remembrance.IsDaily = dto.IsDaily;

            // تحديث الفئات (Many-to-Many)
            if (dto.CategoryIds != null)
            {
                remembrance.RemembranceCategoryLinks.Clear();
                if (dto.CategoryIds.Any())
                {
                    var categories = await _categoryRepository.FindAsync(c => dto.CategoryIds.Contains(c.Id));
                    remembrance.RemembranceCategoryLinks = categories.Select(x => new RemembranceCategoryLinks { RemembranceCategoryId = x.Id }).ToList();
                }
            }

            // تحديث المحتوى
            if (dto.Contents != null)
            {
                remembrance.Contents.Clear();
                remembrance.Contents = dto.Contents.Select(c => new RemembranceContent
                {
                    Id = Guid.NewGuid(),
                    SourceType = c.SourceType,
                    SourceId = c.SourceId,
                    CustomContent = c.CustomContent,
                    RemembranceId = remembrance.Id
                }).ToList();
            }

            remembrance.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            var updated = await _remembranceRepository.GetByIdWithDetailsAsync(id);
            return Result<RemembranceResponseDto>.Success(updated!.ToResponseDto());
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
    }
}