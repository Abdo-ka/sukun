using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.IslamicBook.Request;
using Sukun.Application.Dtos.IslamicBookSection.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class IslamicBookSectionService : IIslamicBookSectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIslamicBookSectionRepository _sectionRepository;
        private readonly IIslamicBookRepository _bookRepository;
        private readonly ILogger<IslamicBookSectionService> _logger;

        public IslamicBookSectionService(
            IUnitOfWork unitOfWork,
            IIslamicBookSectionRepository sectionRepository,
            IIslamicBookRepository bookRepository,
            ILogger<IslamicBookSectionService> logger)
        {
            _unitOfWork = unitOfWork;
            _sectionRepository = sectionRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<IslamicBookSectionResponseDto>>> GetByBookAsync(Guid bookId)
        {
            var bookExists = await _bookRepository.ExistsAsync(b => b.Id == bookId);
            if (!bookExists)
                return Result<IEnumerable<IslamicBookSectionResponseDto>>.NotFound("Book not found");

            var sections = await _sectionRepository.GetByBookAsync(bookId);
            var dtos = sections.Select(s => s.ToSectionDto()).ToList();
            foreach ( var dto in dtos)
            {
                dto.HadithsCount = await _unitOfWork.Hadiths.CountAsync(h => h.SectionId == dto.Id);

            }
            return Result<IEnumerable<IslamicBookSectionResponseDto>>.Success(dtos);
        }

        public async Task<Result<IslamicBookSectionResponseDto>> GetByIdAsync(Guid id)
        {
            var section = await _sectionRepository.GetByIdWithIncludesAsync(id ,x=>x.Contents);
            if (section == null || section.IsDeleted)
                return Result<IslamicBookSectionResponseDto>.NotFound("Section not found");
            var hadithsCount = await _unitOfWork.Hadiths.CountAsync(h => h.SectionId == id);

            var dto = section.ToSectionDto();
            dto.HadithsCount = hadithsCount;

            return Result<IslamicBookSectionResponseDto>.Success(dto);
        }

        public async Task<Result<IslamicBookSectionResponseDto>> CreateAsync(Guid bookId, IslamicBookSectionCreateDto dto)
        {
            var bookExists = await _bookRepository.ExistsAsync(b => b.Id == bookId);
            if (!bookExists)
                return Result<IslamicBookSectionResponseDto>.BadRequest("Invalid BookId");

            var section = new IslamicBookSection
            {
                Id = Guid.NewGuid(),
                BookId = bookId,
                Name = dto.Name,
                NameEn = dto.NameEn,
                Description = dto.Description,
                Order = dto.Order,
                CreateAt = DateTime.UtcNow
            };

            var addResult = await _sectionRepository.AddAsync(section);
            if (!addResult.IsSuccess)
                return Result<IslamicBookSectionResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<IslamicBookSectionResponseDto>.Success(section.ToSectionDto());
        }

        public async Task<Result<IslamicBookSectionResponseDto>> UpdateAsync(Guid id, IslamicBookSectionUpdateDto dto)
        {
            var section = await _sectionRepository.GetByIdAsync(id);
            if (section == null)
                return Result<IslamicBookSectionResponseDto>.NotFound("Section not found");

            if (!string.IsNullOrEmpty(dto.Name)) section.Name = dto.Name;
            if (dto.NameEn != null) section.NameEn = dto.NameEn;
            if (dto.Description != null) section.Description = dto.Description;
            section.Order = dto.Order;

            section.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<IslamicBookSectionResponseDto>.Success(section.ToSectionDto());
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var section = await _sectionRepository.GetByIdAsync(id);
            if (section == null)
                return Result.NotFound("Section not found");

            section.IsDeleted = true;
            section.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}