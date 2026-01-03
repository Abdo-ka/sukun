using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.RemembranceCategory.Request;
using Sukun.Application.Dtos.RemembranceCategory.Response;
using Sukun.Application.Dtos.RemembranceContent.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class RemembranceCategoryService : IRemembranceCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRemembranceCategoryRepository _categoryRepository;
        private readonly ISourceResolverService _sourceResolver;
        private readonly ILogger<RemembranceCategoryService> _logger;

        public RemembranceCategoryService(
            IUnitOfWork unitOfWork,
            IRemembranceCategoryRepository categoryRepository,
            ISourceResolverService sourceResolver,
            ILogger<RemembranceCategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _sourceResolver = sourceResolver;
        }

        public async Task<Result<IEnumerable<RemembranceCategoryResponseDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllWithRemembrancesAsync();
            var dtos = categories.Select(c => c.ToResponseDto());
            return Result<IEnumerable<RemembranceCategoryResponseDto>>.Success(dtos);
        }

        public async Task<Result<RemembranceCategoryWithRemembrancesDto>> GetByIdWithRemembrancesAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.IsDeleted)
                return Result<RemembranceCategoryWithRemembrancesDto>.NotFound("Category not found");

            var remembrances = await _unitOfWork.Remembrances.GetByCategoryWithFullDetailsAsync(id);

            var allContents = remembrances
              .SelectMany(r => r.Contents)
              .Where(c => !c.IsDeleted)
              .ToList();

            var sourcePreviews = allContents.Any()
                ? await _sourceResolver.ResolveAsync(allContents)
                : new Dictionary<Guid, SourcePreviewDto>();

            var dto = category.ToCategoryWithRemembrancesDto(remembrances, sourcePreviews);
            return Result<RemembranceCategoryWithRemembrancesDto>.Success(dto);
        }


        public async Task<Result<RemembranceCategoryResponseDto>> CreateAsync(RemembranceCategoryCreateDto dto)
        {
            var category = dto.ToEntity();

            var addResult = await _categoryRepository.AddAsync(category);
            if (!addResult.IsSuccess)
                return Result<RemembranceCategoryResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<RemembranceCategoryResponseDto>.Success(category.ToResponseDto());
        }

        public async Task<Result<RemembranceCategoryResponseDto>> UpdateAsync(Guid id, RemembranceCategoryUpdateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return Result<RemembranceCategoryResponseDto>.NotFound("Category not found");

            if (!string.IsNullOrEmpty(dto.Name)) category.Name = dto.Name;
            if (dto.NameEn != null) category.NameEn = dto.NameEn;
            category.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<RemembranceCategoryResponseDto>.Success(category.ToResponseDto());
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return Result.NotFound("Category not found");

            category.IsDeleted = true;
            category.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}