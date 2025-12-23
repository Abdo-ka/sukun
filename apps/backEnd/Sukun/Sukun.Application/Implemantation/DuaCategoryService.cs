using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Dua.Response;
using Sukun.Application.Dtos.DuaCategory.Request;
using Sukun.Application.Dtos.DuaCategory.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class DuaCategoryService : IDuaCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDuaCategoryRepository _categoryRepository;
        private readonly ILogger<DuaCategoryService> _logger;

        public DuaCategoryService(
            IUnitOfWork unitOfWork,
            IDuaCategoryRepository categoryRepository,
            ILogger<DuaCategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<DuaCategoryResponseDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllWithDuasCountAsync();
            var dtos = categories.Select(c => c.ToResponseDto());
            return Result<IEnumerable<DuaCategoryResponseDto>>.Success(dtos);
        }

        public async Task<Result<DuaCategoryWithDuasDto>> GetByIdWithDuasAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.IsDeleted)
                return Result<DuaCategoryWithDuasDto>.NotFound("Category not found");

            // تحميل الأدعية
            var duas = await _unitOfWork.Repository<DuaItem>()
                .FindAsync(d => d.CategoryId == id && !d.IsDeleted);

            var dto = category.ToCategoryWithDuasDto();
            dto.Duas = duas.Select(d => d.ToResponseDto()).ToList();

            return Result<DuaCategoryWithDuasDto>.Success(dto);
        }

        // ====================== Admin ======================

        public async Task<Result<DuaCategoryResponseDto>> CreateAsync(DuaCategoryCreateDto dto)
        {
            var category = dto.ToEntity();

            var addResult = await _categoryRepository.AddAsync(category);
            if (!addResult.IsSuccess)
                return Result<DuaCategoryResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<DuaCategoryResponseDto>.Success(category.ToResponseDto());
        }

        public async Task<Result<DuaCategoryResponseDto>> UpdateAsync(Guid id, DuaCategoryUpdateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return Result<DuaCategoryResponseDto>.NotFound("Category not found");

            if (!string.IsNullOrEmpty(dto.Name)) category.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.NameAr)) category.NameAr = dto.NameAr;
            if (dto.NameEn != null) category.NameEn = dto.NameEn;

            category.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<DuaCategoryResponseDto>.Success(category.ToResponseDto());
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