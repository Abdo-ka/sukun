using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.BookContent.Request;
using Sukun.Application.Dtos.BookContent.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public class BookContentService : IBookContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<BookContent> _contentRepository;
        private readonly IIslamicBookSectionRepository _sectionRepository;
        private readonly ILogger<BookContentService> _logger;

        public BookContentService(
            IUnitOfWork unitOfWork,
            IIslamicBookSectionRepository sectionRepository,
            ILogger<BookContentService> logger)
        {
            _unitOfWork = unitOfWork;
            _contentRepository = unitOfWork.Repository<BookContent>();
            _sectionRepository = sectionRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<BookContentResponseDto>>> GetBySectionAsync(Guid sectionId)
        {
            var sectionExists = await _sectionRepository.ExistsAsync(s => s.Id == sectionId);
            if (!sectionExists)
                return Result<IEnumerable<BookContentResponseDto>>.NotFound("Section not found");

            var contents = await _contentRepository.AsQueryable().Where(
                c => !c.IsDeleted && c.SectionId == sectionId).
                OrderBy(c => c.DisplayOrder).ToListAsync();

            return Result<IEnumerable<BookContentResponseDto>>.Success(
                contents.Select(c => c.ToContentDto()));
        }

        public async Task<Result<BookContentResponseDto>> GetByIdAsync(Guid id)
        {
            var content = await _contentRepository.GetByIdAsync(id);
            if (content == null || content.IsDeleted)
                return Result<BookContentResponseDto>.NotFound("Content not found");

            return Result<BookContentResponseDto>.Success(content.ToContentDto());
        }

        // ====================== Admin Operations ======================

        public async Task<Result<BookContentResponseDto>> CreateAsync(Guid sectionId, BookContentCreateDto dto)
        {
            var sectionExists = await _sectionRepository.ExistsAsync(s => s.Id == sectionId);
            if (!sectionExists)
                return Result<BookContentResponseDto>.BadRequest("Invalid SectionId");

            var content = new BookContent
            {
                Id = Guid.NewGuid(),
                SectionId = sectionId,
                Title = dto.Title,
                Content = dto.Content,
                DisplayOrder = dto.DisplayOrder,
                CreateAt = DateTime.UtcNow
            };

            var addResult = await _contentRepository.AddAsync(content);
            if (!addResult.IsSuccess)
                return Result<BookContentResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<BookContentResponseDto>.Success(content.ToContentDto());
        }

        public async Task<Result<BookContentResponseDto>> UpdateAsync(Guid id, BookContentUpdateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(id);
            if (content == null)
                return Result<BookContentResponseDto>.NotFound("Content not found");

            if (!string.IsNullOrEmpty(dto.Title)) content.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Content)) content.Content = dto.Content;
            content.DisplayOrder = dto.DisplayOrder;

            content.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<BookContentResponseDto>.Success(content.ToContentDto());
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var content = await _contentRepository.GetByIdAsync(id);
            if (content == null)
                return Result.NotFound("Content not found");

            content.IsDeleted = true;
            content.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}
