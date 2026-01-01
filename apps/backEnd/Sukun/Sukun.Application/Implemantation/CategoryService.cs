using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.NarrativeCategory.Request;
using Sukun.Application.Dtos.NarrativeCategory.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepo = unitOfWork.Categories;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<CategoryResponseDto>>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetAllAsync();
                var dtos = categories.Select(c => c.ToResponseDto());
                return Result<IEnumerable<CategoryResponseDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving main categories");
                return Result<IEnumerable<CategoryResponseDto>>.BadRequest("An error occurred");
            }
        }
        public async Task<Result<IEnumerable<CategoryResponseDto>>> GetMainSectionsAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetMainSectionsAsync();
                var dtos = categories.Select(c => c.ToResponseDto());
                return Result<IEnumerable<CategoryResponseDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving main categories");
                return Result<IEnumerable<CategoryResponseDto>>.BadRequest("An error occurred");
            }
        }

        public async Task<Result<CategoryWithChildrenResponseDto>> GetByIdWithChildrenAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepo.GetByIdWithChildrenAsync(id);
                if (category == null)
                    return Result<CategoryWithChildrenResponseDto>.NotFound("Category not found");

                var dto = category.ToWithChildrenDto();
                return Result<CategoryWithChildrenResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category with children {Id}", id);
                return Result<CategoryWithChildrenResponseDto>.BadRequest("An error occurred");
            }
        }

        public async Task<Result<CategoryWithNarrativesResponseDto>> GetByIdWithNarrativesAsync(Guid id)
        {
            try
            {
                var exists = await _unitOfWork.Categories.ExistsAsync(c => c.Id == id);
                if (!exists)
                    return Result<CategoryWithNarrativesResponseDto>.NotFound("Category not found");

                var response = await _unitOfWork.NarrativeCategories.AsQueryable()
                             .Where(h => h.CategoryId == id)
                             .Include(x => x.Narrative)
                             .Include(x => x.Category)
                             .Select(x=>x.Category.ToWithNarrativeDto())
                             .FirstOrDefaultAsync();

                return Result<CategoryWithNarrativesResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category narratives {Id}", id);
                return Result<CategoryWithNarrativesResponseDto>.BadRequest("An error occurred");
            }
        }


        public async Task<Result<CategoryResponseDto>> CreateAsync(CategoryCreateDto dto)
        {
            try
            {
                var category = dto.ToEntity();
                var addResult = await _categoryRepo.AddAsync(category);
                if (!addResult.IsSuccess)
                    return Result<CategoryResponseDto>.Failure(addResult.Message);

                await _unitOfWork.CompleteAsync();

                var created = await _categoryRepo.GetByIdAsync(category.Id);
                return Result<CategoryResponseDto>.Success(created!.ToResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating narrative");
                return Result<CategoryResponseDto>.BadRequest("An error occurred while creating the narrative");
            }
        }

        public async Task<Result<CategoryResponseDto>> UpdateAsync(Guid id, CategoryUpdateDto dto)
        {
            try
            {
                var category = await _categoryRepo.GetByIdAsync(id);
                if (category == null)
                    return Result<CategoryResponseDto>.NotFound("Narrative not found");

                // تحديث الحقول الأساسية
                if (!string.IsNullOrEmpty(dto.Title)) category.Title = dto.Title;
                if (dto.TitleAr != null) category.TitleAr = dto.TitleAr;

                category.UpdatedAt = DateTime.UtcNow;

                var updateResult = await _categoryRepo.UpdateAsync(category);
                if (!updateResult.IsSuccess)
                    return Result<CategoryResponseDto>.Failure(updateResult.Message);

                await _unitOfWork.CompleteAsync();

                var updated = await _categoryRepo.GetByIdAsync(id);
                return Result<CategoryResponseDto>.Success(updated!.ToResponseDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating narrative {Id}", id);
                return Result<CategoryResponseDto>.BadRequest("An error occurred while updating the narrative");
            }
        }

        public async Task<Result<bool>> SoftDeleteAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepo.GetByIdAsync(id);
                if (category == null)
                    return Result<bool>.NotFound("category not found");

                var deleteResult = await _categoryRepo.DeleteAsync(category);
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

        public async Task<Result<IEnumerable<CategoryResponseDto>>> GetRootAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetRootAsync();
                var dtos = categories.Select(c => c.ToResponseDto());
                return Result<IEnumerable<CategoryResponseDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving main categories");
                return Result<IEnumerable<CategoryResponseDto>>.BadRequest("An error occurred");
            }
        }

    
    }
}