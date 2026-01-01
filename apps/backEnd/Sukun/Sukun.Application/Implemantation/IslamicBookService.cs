using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.BookContent.Response;
using Sukun.Application.Dtos.IslamicBook.Response;
using Sukun.Application.Dtos.IslamicBookSection.Request;
using Sukun.Application.Dtos.IslamicBookSection.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;
using Sukun.Infrastructure.Repositories;

namespace Sukun.Application.Implemantation
{
    public class IslamicBookService : IIslamicBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIslamicBookRepository _bookRepository;
        private readonly ILogger<IslamicBookService> _logger;

        public IslamicBookService(
            IUnitOfWork unitOfWork,
            IIslamicBookRepository bookRepository,
            ILogger<IslamicBookService> logger)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<IslamicBookListDto>>> GetAllAsync()
        {
            try
            {
                var booksQuery = _bookRepository.AsQueryableNoTracking(); // يجب أن يرجع IQueryable<IslamicBook>
                var result = await booksQuery
                .GroupJoin(
                        _unitOfWork.Hadiths.AsQueryableNoTracking().Where(h => !h.IsDeleted),
                        book => book.Id,
                        hadith => hadith.BookId,
                        (book, hadiths) => new IslamicBookListDto
                        {
                            Id = book.Id,
                            Name = book.Name,
                            NameAr = book.NameAr,
                            NameEn = book.NameEn,
                            Author = book.Author,
                            Type = book.Type,
                            Order = book.Order,
                            HadithsCount = hadiths.Count()
                        })
                    .OrderBy(b => b.Order) 
                    .ToListAsync();

                return Result<IEnumerable<IslamicBookListDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IslamicBookListDto>>.BadRequest("An error occurred while retrieving books");
            }
        }

        public async Task<Result<IEnumerable<IslamicBookListDto>>> GetByTypeAsync(BookType type)
        {
            var books = await _bookRepository.GetByTypeAsync(type);
            var dtos = books.Select(b => b.ToListDto()).ToList();
            foreach (var d in dtos)
            {
                d.HadithsCount = await _unitOfWork.Hadiths.CountAsync(h=>h.BookId == d.Id);
                
            }

            return Result<IEnumerable<IslamicBookListDto>>.Success(dtos);
        }

        public async Task<Result<IslamicBookResponseDto>> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdWithIncludesAsync(id,x=>x.Sections);
            if (book == null || book.IsDeleted)
                return Result<IslamicBookResponseDto>.NotFound("Book not found");

            var hadithsCount = await _unitOfWork.Hadiths.CountAsync(h =>h.BookId == id);
            var sectionHadithsCounts = await _unitOfWork.Hadiths.AsQueryableNoTracking()
            .Where(h => !h.IsDeleted && h.SectionId != null && h.BookId == id)
            .GroupBy(h => h.SectionId.Value)
            .Select(g => new { SectionId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.SectionId, x => x.Count);
            var dto = book.ToResponseDto();
            dto.HadithsCount = hadithsCount;
            dto.SectionsCount = book.Sections.Count();
            foreach (var sectionDto in dto.Sections)
            {
                sectionDto.HadithsCount = sectionHadithsCounts.TryGetValue(sectionDto.Id, out var count)
                    ? count
                    : 0;
            }
            return Result<IslamicBookResponseDto>.Success(dto);
        }

        public async Task<Result<IslamicBookResponseDto>> GetByIdWithSectionsAsync(Guid id)
        {
            try
            {
                var book = await _bookRepository.GetByIdWithSectionsAsync(id);

                if (book == null || book.IsDeleted)
                    return Result<IslamicBookResponseDto>.NotFound("Book not found");

                var totalHadithsCount = await _unitOfWork.Hadiths
                    .CountAsync(h=>h.BookId == id);

                var sectionHadithsCounts = await _unitOfWork.Hadiths.AsQueryableNoTracking()
                                .Where(h => !h.IsDeleted && h.SectionId != null && h.BookId == id)
                                .GroupBy(h => h.SectionId.Value)
                                .Select(g => new { SectionId = g.Key, Count = g.Count() })
                                .ToDictionaryAsync(x => x.SectionId, x => x.Count);
                var responseDto = book.ToResponseDto();

                foreach (var sectionDto in responseDto.Sections)
                {
                    sectionDto.HadithsCount = sectionHadithsCounts.TryGetValue(sectionDto.Id, out var count)
                        ? count
                        : 0;
                }
                responseDto.HadithsCount = totalHadithsCount;
                responseDto.SectionsCount = book.Sections.Count();

                return Result<IslamicBookResponseDto>.Success(responseDto);
            }
            catch (Exception)
            {
                return Result<IslamicBookResponseDto>.BadRequest("An error occurred while retrieving the book");
            }
        }

        // ====================== Admin ======================

        public async Task<Result<IslamicBookResponseDto>> CreateAsync(IslamicBookCreateDto dto)
        {
            var book = dto.ToEntity();

            var addResult = await _bookRepository.AddAsync(book);
            if (!addResult.IsSuccess)
                return Result<IslamicBookResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<IslamicBookResponseDto>.Success(book.ToResponseDto());
        }

        public async Task<Result<IslamicBookResponseDto>> UpdateAsync(Guid id, IslamicBookUpdateDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return Result<IslamicBookResponseDto>.NotFound("Book not found");

            if (!string.IsNullOrEmpty(dto.Name)) book.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.NameAr)) book.NameAr = dto.NameAr;
            if (dto.NameEn != null) book.NameEn = dto.NameEn;
            if (dto.Author != null) book.Author = dto.Author;
            if (dto.Description != null) book.Description = dto.Description;
            if (dto.Type != 0) book.Type = dto.Type;
            book.Order = dto.Order;

            book.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<IslamicBookResponseDto>.Success(book.ToResponseDto());
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return Result.NotFound("Book not found");

            book.IsDeleted = true;
            book.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}