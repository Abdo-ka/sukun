using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.IslamicBook.Response;
using Sukun.Application.Dtos.IslamicBookSection.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

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
            var books = await _bookRepository.GetAllWithDetailsAsync();
            return Result<IEnumerable<IslamicBookListDto>>.Success(books.Select(b => b.ToListDto()));
        }

        public async Task<Result<IEnumerable<IslamicBookListDto>>> GetByTypeAsync(BookType type)
        {
            var books = await _bookRepository.GetByTypeAsync(type);
            return Result<IEnumerable<IslamicBookListDto>>.Success(books.Select(b => b.ToListDto()));
        }

        public async Task<Result<IslamicBookResponseDto>> GetByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null || book.IsDeleted)
                return Result<IslamicBookResponseDto>.NotFound("Book not found");

            return Result<IslamicBookResponseDto>.Success(book.ToResponseDto());
        }

        public async Task<Result<IslamicBookResponseDto>> GetByIdWithSectionsAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdWithSectionsAsync(id);
            if (book == null || book.IsDeleted)
                return Result<IslamicBookResponseDto>.NotFound("Book not found");

            return Result<IslamicBookResponseDto>.Success(book.ToResponseDto());
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