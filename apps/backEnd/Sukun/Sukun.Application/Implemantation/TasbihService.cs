using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Tasbih.Request;
using Sukun.Application.Dtos.Tasbih.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class TasbihService : ITasbihService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITasbihRepository _tasbihRepository;
        private readonly ILogger<TasbihService> _logger;

        public TasbihService(
            IUnitOfWork unitOfWork,
            ITasbihRepository tasbihRepository,
            ILogger<TasbihService> logger)
        {
            _unitOfWork = unitOfWork;
            _tasbihRepository = tasbihRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<TasbihResponseDto>>> GetAllAsync()
        {
            var tasbihs = await _tasbihRepository.GetAllOrderedAsync();
            return Result<IEnumerable<TasbihResponseDto>>.Success(tasbihs.Select(t => t.ToResponseDto()));
        }

        public async Task<Result<TasbihResponseDto>> GetByIdAsync(Guid id)
        {
            var tasbih = await _tasbihRepository.GetByIdAsync(id);
            if (tasbih == null || tasbih.IsDeleted)
                return Result<TasbihResponseDto>.NotFound("Tasbih not found");

            return Result<TasbihResponseDto>.Success(tasbih.ToResponseDto());
        }

        public async Task<Result<TasbihResponseDto>> GetRandomAsync()
        {
            var tasbih = await _tasbihRepository.GetRandomAsync();
            if (tasbih == null)
                return Result<TasbihResponseDto>.NotFound("No tasbih available");

            return Result<TasbihResponseDto>.Success(tasbih.ToResponseDto());
        }

        // ====================== Admin ======================

        public async Task<Result<TasbihResponseDto>> CreateAsync(TasbihCreateDto dto)
        {
            var tasbih = dto.ToEntity();

            var addResult = await _tasbihRepository.AddAsync(tasbih);
            if (!addResult.IsSuccess)
                return Result<TasbihResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<TasbihResponseDto>.Success(tasbih.ToResponseDto());
        }

        public async Task<Result<TasbihResponseDto>> UpdateAsync(Guid id, TasbihUpdateDto dto)
        {
            var tasbih = await _tasbihRepository.GetByIdAsync(id);
            if (tasbih == null)
                return Result<TasbihResponseDto>.NotFound("Tasbih not found");

            if (!string.IsNullOrEmpty(dto.Title)) tasbih.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.TitleAr)) tasbih.TitleAr = dto.TitleAr;
            if (dto.Benefits != null) tasbih.Benefits = dto.Benefits;
            tasbih.RecommendedCount = dto.RecommendedCount;
            tasbih.Order = dto.Order;

            tasbih.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<TasbihResponseDto>.Success(tasbih.ToResponseDto());
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var tasbih = await _tasbihRepository.GetByIdAsync(id);
            if (tasbih == null)
                return Result.NotFound("Tasbih not found");

            tasbih.IsDeleted = true;
            tasbih.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}