using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class HadithCategoryService : IHadithCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHadithCategoryRepository _categoryRepository;
        private readonly ILogger<HadithCategoryService> _logger;

        public HadithCategoryService(
            IUnitOfWork unitOfWork,
            IHadithCategoryRepository categoryRepository,
            ILogger<HadithCategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<HadithCategoryResponseDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllWithHadithsCountAsync();
            return Result<IEnumerable<HadithCategoryResponseDto>>.Success(
                categories.Select(c => c.ToResponseDto()));
        }

        public async Task<Result<HadithCategoryResponseDto>> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.IsDeleted)
                return Result<HadithCategoryResponseDto>.NotFound("Category not found");

            // تحميل عدد الأحاديث  ?? 
            var count = await _unitOfWork.Repository<Hadith>()
                .CountAsync(h => h.CategoryId == id && !h.IsDeleted);

            var dto = category.ToResponseDto();
            dto.HadithsCount = count;

            return Result<HadithCategoryResponseDto>.Success(dto);
        }

        public async Task<Result<int>> GetHadithsCountAsync(Guid categoryId)
        {
            var count = await _unitOfWork.Repository<Hadith>()
                .CountAsync(h => h.CategoryId == categoryId && !h.IsDeleted);
            return Result<int>.Success(count);
        }

        // ====================== Admin ======================

        public async Task<Result<HadithCategoryResponseDto>> CreateAsync(HadithCategoryCreateDto dto)
        {
            var category = new HadithCategory
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreateAt = DateTime.UtcNow
            };

            var addResult = await _categoryRepository.AddAsync(category);
            if (!addResult.IsSuccess)
                return Result<HadithCategoryResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<HadithCategoryResponseDto>.Success(category.ToResponseDto());
        }

        public async Task<Result<HadithCategoryResponseDto>> UpdateAsync(Guid id, HadithCategoryUpdateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return Result<HadithCategoryResponseDto>.NotFound("Category not found");

            if (!string.IsNullOrEmpty(dto.Name)) category.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description)) category.Description = dto.Description;

            category.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<HadithCategoryResponseDto>.Success(category.ToResponseDto());
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