using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Dua.Request;
using Sukun.Application.Dtos.Dua.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class DuaItemService : IDuaItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDuaItemRepository _duaRepository;
        private readonly IDuaCategoryRepository _categoryRepository;
        private readonly ILogger<DuaItemService> _logger;

        public DuaItemService(
            IUnitOfWork unitOfWork,
            IDuaItemRepository duaRepository,
            IDuaCategoryRepository categoryRepository,
            ILogger<DuaItemService> logger)
        {
            _unitOfWork = unitOfWork;
            _duaRepository = duaRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<DuaItemResponseDto>>> GetByCategoryAsync(Guid categoryId)
        {
            var exists = await _categoryRepository.ExistsAsync(c => c.Id == categoryId);
            if (!exists)
                return Result<IEnumerable<DuaItemResponseDto>>.NotFound("Category not found");

            var duas = await _duaRepository.GetByCategoryAsync(categoryId);
            return Result<IEnumerable<DuaItemResponseDto>>.Success(duas.Select(d => d.ToResponseDto()));
        }

        public async Task<Result<DuaItemResponseDto>> GetByIdAsync(Guid id)
        {
            var dua = await _duaRepository.GetByIdAsync(id);
            if (dua == null || dua.IsDeleted)
                return Result<DuaItemResponseDto>.NotFound("Dua not found");

            return Result<DuaItemResponseDto>.Success(dua.ToResponseDto());
        }

        public async Task<Result<DuaItemResponseDto>> GetRandomAsync()
        {
            var randomDua = await _duaRepository.GetRandomAsync();

            if (randomDua == null)
                return Result<DuaItemResponseDto>.NotFound("No duas available");

            return Result<DuaItemResponseDto>.Success(randomDua.ToResponseDto());
        }

        // ====================== Admin ======================

        public async Task<Result<DuaItemResponseDto>> CreateAsync(DuaItemCreateDto dto)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
                return Result<DuaItemResponseDto>.BadRequest("Invalid CategoryId");

            var dua = dto.ToEntity();

            var addResult = await _duaRepository.AddAsync(dua);
            if (!addResult.IsSuccess)
                return Result<DuaItemResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<DuaItemResponseDto>.Success(dua.ToResponseDto());
        }

        public async Task<Result<DuaItemResponseDto>> UpdateAsync(Guid id, DuaItemUpdateDto dto)
        {
            var dua = await _duaRepository.GetByIdAsync(id);
            if (dua == null)
                return Result<DuaItemResponseDto>.NotFound("Dua not found");

            if (!string.IsNullOrEmpty(dto.Title)) dua.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.ArabicText)) dua.ArabicText = dto.ArabicText;
            if (dto.Transliteration != null) dua.Transliteration = dto.Transliteration;
            if (dto.Translation != null) dua.Translation = dto.Translation;
            if (dto.Reference != null) dua.Reference = dto.Reference;
            if (dto.RepeatCount.HasValue) dua.RepeatCount = dto.RepeatCount;
            if (dto.Virtue != null) dua.Virtue = dto.Virtue;
            dua.DisplayOrder = dto.DisplayOrder;

            dua.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<DuaItemResponseDto>.Success(dua.ToResponseDto());
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var dua = await _duaRepository.GetByIdAsync(id);
            if (dua == null)
                return Result.NotFound("Dua not found");

            dua.IsDeleted = true;
            dua.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}