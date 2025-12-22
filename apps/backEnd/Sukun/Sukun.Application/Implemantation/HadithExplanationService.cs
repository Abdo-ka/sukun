using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class HadithExplanationService : IHadithExplanationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<HadithExplanation> _explanationRepository;
        private readonly IHadithRepository _hadithRepository;
        private readonly ILogger<HadithExplanationService> _logger;

        public HadithExplanationService(
            IUnitOfWork unitOfWork,
            IHadithRepository hadithRepository,
            ILogger<HadithExplanationService> logger)
        {
            _unitOfWork = unitOfWork;
            _hadithRepository = hadithRepository;
            _explanationRepository = unitOfWork.Repository<HadithExplanation>();
            _logger = logger;
        }

        public async Task<Result<IEnumerable<HadithExplanationResponseDto>>> GetByHadithAsync(Guid hadithId)
        {
            var exists = await _hadithRepository.ExistsAsync(h => h.Id == hadithId);
            if (!exists)
                return Result<IEnumerable<HadithExplanationResponseDto>>.NotFound("Hadith not found");

            var explanations = await _explanationRepository.FindAsync(e => e.HadithId == hadithId && !e.IsDeleted);
            return Result<IEnumerable<HadithExplanationResponseDto>>.Success(
                explanations.Select(e => e.ToResponseDto()));
        }

        public async Task<Result<HadithExplanationResponseDto>> GetByIdAsync(Guid id)
        {
            var explanation = await _explanationRepository.GetByIdAsync(id);
            if (explanation == null || explanation.IsDeleted)
                return Result<HadithExplanationResponseDto>.NotFound("Explanation not found");

            return Result<HadithExplanationResponseDto>.Success(explanation.ToResponseDto());
        }

        public async Task<Result<HadithExplanationResponseDto>> CreateAsync(Guid hadithId, HadithExplanationCreateDto dto)
        {
            var hadithExists = await _hadithRepository.ExistsAsync(h => h.Id == hadithId);
            if (!hadithExists)
                return Result<HadithExplanationResponseDto>.BadRequest("Hadith not found");

            var explanation = new HadithExplanation
            {
                Id = Guid.NewGuid(),
                HadithId = hadithId,
                Scholar = dto.Scholar,
                Explanation = dto.Explanation,
                CreateAt = DateTime.UtcNow
            };

            var addResult = await _explanationRepository.AddAsync(explanation);
            if (!addResult.IsSuccess)
                return Result<HadithExplanationResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();
            return Result<HadithExplanationResponseDto>.Success(explanation.ToResponseDto());
        }

        public async Task<Result<HadithExplanationResponseDto>> UpdateAsync(Guid id, HadithExplanationUpdateDto dto)
        {
            var explanation = await _explanationRepository.GetByIdAsync(id);
            if (explanation == null)
                return Result<HadithExplanationResponseDto>.NotFound("Explanation not found");

            if (!string.IsNullOrEmpty(dto.Scholar)) explanation.Scholar = dto.Scholar;
            if (!string.IsNullOrEmpty(dto.Explanation)) explanation.Explanation = dto.Explanation;

            explanation.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result<HadithExplanationResponseDto>.Success(explanation.ToResponseDto());
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var explanation = await _explanationRepository.GetByIdAsync(id);
            if (explanation == null)
                return Result.NotFound("Explanation not found");

            explanation.IsDeleted = true;
            explanation.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}