using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.AsmaulHusna.Responce;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class AsmaulHusnaService : BaseService, IAsmaulHusnaService
    {
        private readonly IAsmaulHusnaRepository _asmaulHusnaRepository;

        public AsmaulHusnaService(IUnitOfWork unitOfWork, ILogger<AsmaulHusnaService> logger)
            : base(unitOfWork, logger)
        {
            _asmaulHusnaRepository = unitOfWork.AsmaulHusna; // أو Repository<AsmaulHusna>() إذا لم تضف Specific
        }

        public async Task<Result<IEnumerable<AsmaulHusnaResponseDto>>> GetAllAsync()
        {
            var names = await _asmaulHusnaRepository.GetAllAsync();
            return Result<IEnumerable<AsmaulHusnaResponseDto>>.Success(names.ToResponseDtos());
        }

        public async Task<Result<AsmaulHusnaResponseDto>> GetByNumberAsync(int number)
        {
            if (number < 1 || number > 99)
                return Result<AsmaulHusnaResponseDto>.Failure("Number must be between 1 and 99");

            var name = await _asmaulHusnaRepository.GetByNumberAsync(number);
            if (name == null)
                return Result<AsmaulHusnaResponseDto>.NotFound("Name not found");

            return Result<AsmaulHusnaResponseDto>.Success(name.ToResponseDto());
        }

        public async Task<Result<AsmaulHusnaResponseDto>> GetRandomAsync()
        {
            var names = await _asmaulHusnaRepository.GetRandomAsync(1);
            var name = names.FirstOrDefault();
            if (name == null)
                return Result<AsmaulHusnaResponseDto>.NotFound("No names available");

            return Result<AsmaulHusnaResponseDto>.Success(name.ToResponseDto());
        }
    }
}