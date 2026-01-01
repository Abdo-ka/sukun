using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Hadith.Request;
using Sukun.Application.Dtos.Hadith.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class HadithService : IHadithService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHadithRepository _hadithRepository;
        private readonly IHadithCategoryRepository _categoryRepository;
        private readonly IHadithExplanationRepository _hadithExplanationRepository;
        private readonly ILogger<HadithService> _logger;

        public HadithService(
            IUnitOfWork unitOfWork,
            IHadithCategoryRepository categoryRepository,
            IHadithRepository hadithRepository,
            IHadithExplanationRepository hadithExplanation,
            ILogger<HadithService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _hadithRepository = hadithRepository;
            _hadithExplanationRepository = hadithExplanation;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<HadithCategoryResponseDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllWithHadithsCountAsync();
            var dtos = categories.Select(c => c.ToResponseDto());
            return Result<IEnumerable<HadithCategoryResponseDto>>.Success(dtos);
        }

        public async Task<Result<PagedResponseDto<HadithListResponseDto>>> GetPagedAsync(PagedRequestDto request, Guid? bookId)
        {
            if (request.PageNumber < 1) request.PageNumber = 1;
            if (request.PageSize < 1 || request.PageSize > 100) request.PageSize = 20;

            var query = _hadithRepository.AsQueryable();

            query = query.Include(x => x.Book)
                 .Include(x => x.Category)
                 .Include(x => x.Section)
                 .Include(x => x.Explanations);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(h =>
                    h.Text.ToLower().Contains(term) ||
                    h.Reference.ToLower().Contains(term) ||
                    h.Section.Name.ToLower().Contains(term) ||
                    (h.Book.Name != null && h.Book.Name.ToLower().Contains(term)));
            }
            if (bookId.HasValue)
            {
                query = query.Where(h => h.BookId == bookId);
            }

            // query = query.OrderBy(h => GetSortValue(h.HadithNumber));


            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(h => h.ToListDto())
                .ToListAsync();

            var paged = new PagedResponseDto<HadithListResponseDto>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                HasPreviousPage = request.PageNumber > 1,
                HasNextPage = request.PageNumber < (int)Math.Ceiling(totalCount / (double)request.PageSize),
                Items = items
            };

            return Result<PagedResponseDto<HadithListResponseDto>>.Success(paged);
        }
        private static int GetSortValue(string hadithNumber)
        {
            if (string.IsNullOrEmpty(hadithNumber)) return 99999;
            var parts = hadithNumber.Split(',');
            var numberPart = parts[0].Trim();
            string numbers = " ";
            foreach (var c in numberPart)
            {
                if (c >= '0' && c <= '9')
                    numbers += c;
                else
                    break;
            }
            if (string.IsNullOrEmpty(numbers)) return 99999;
            return int.Parse(numbers);

        }
        public async Task<Result<IEnumerable<HadithListResponseDto>>> GetByCategoryAsync(Guid categoryId)
        {
            var exists = await _categoryRepository.ExistsAsync(c => c.Id == categoryId);
            if (!exists)
                return Result<IEnumerable<HadithListResponseDto>>.NotFound("Category not found");

            var hadiths = await _hadithRepository.GetByCategoryAsync(categoryId);
            return Result<IEnumerable<HadithListResponseDto>>.Success(hadiths.Select(h => h.ToListDto()));
        }

        public async Task<Result<HadithResponseDto>> GetByIdAsync(Guid id)
        {
            var hadith = await _hadithRepository.GetByIdWithDetailsAsync(id);
            if (hadith == null)
                return Result<HadithResponseDto>.NotFound("Hadith not found");

            // اختياري: زيادة عدد المشاهدات
            // hadith.ViewCount++; // إذا أضفت الحقل

            return Result<HadithResponseDto>.Success(hadith.ToResponseDto());
        }

        public async Task<Result<IEnumerable<HadithListResponseDto>>> GetRandomAsync(int count = 5)
        {
            if (count < 1) count = 5;
            if (count > 50) count = 50;

            var hadiths = await _hadithRepository.GetRandomAsync(count);
            return Result<IEnumerable<HadithListResponseDto>>.Success(hadiths.Select(h => h.ToListDto()));
        }

        public async Task<Result<HadithResponseDto>> CreateAsync(HadithCreateDto dto)
        {
            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _categoryRepository.ExistsAsync(c => c.Id == dto.CategoryId.Value);
                if (!categoryExists)
                    return Result<HadithResponseDto>.BadRequest("The specified category does not exist");
            }

            Guid? bookId = dto.BookId;
            Guid? sectionId = dto.SectionId;

            if (sectionId.HasValue && !bookId.HasValue)
            {
                return Result<HadithResponseDto>.BadRequest("BookId is required when SectionId is specified");
            }

            if (bookId.HasValue)
            {
                var bookExists = await _unitOfWork.IslamicBook.ExistsAsync(b => b.Id == bookId.Value);
                if (!bookExists)
                    return Result<HadithResponseDto>.BadRequest("The specified book does not exist");
            }

            if (sectionId.HasValue)
            {
                var sectionExists = await _unitOfWork.IslamicBookSection
                    .ExistsAsync(s => s.Id == sectionId.Value && s.BookId == bookId.Value);

                if (!sectionExists)
                    return Result<HadithResponseDto>.BadRequest("The specified section does not belong to the selected book");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var hadith = dto.ToEntity();

                var addResult = await _hadithRepository.AddAsync(hadith);
                if (!addResult.IsSuccess)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Result<HadithResponseDto>.Failure(addResult.Message);
                }

                await _unitOfWork.CommitTransactionAsync();

                var created = await _hadithRepository.GetByIdWithDetailsAsync(hadith.Id);
                if (created == null)
                    return Result<HadithResponseDto>.BadRequest("Failed to retrieve the created hadith");

                return Result<HadithResponseDto>.Success(created.ToResponseDto());
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<HadithResponseDto>.BadRequest("An error occurred while creating the hadith");
            }
        }
        public async Task<Result<HadithResponseDto>> UpdateAsync(Guid id, HadithUpdateDto dto)
        {
            var hadith = await _hadithRepository.GetByIdWithDetailsAsync(id);
            if (hadith == null)
                return Result<HadithResponseDto>.NotFound("Hadith not found");
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                Guid? newBookId = hadith.BookId;
                Guid? newSectionId = hadith.SectionId;
                if (dto.CategoryId.HasValue && dto.CategoryId != hadith.CategoryId)
                {
                    var categoryExists = await _categoryRepository.ExistsAsync(c => c.Id == dto.CategoryId.Value);
                    if (!categoryExists)
                        return Result<HadithResponseDto>.BadRequest("The specified category does not exist");

                    hadith.CategoryId = dto.CategoryId.Value;
                }
                else if (dto.CategoryId == null && hadith.CategoryId.HasValue)
                {
                    hadith.CategoryId = null;
                }
               
                if (dto.BookId.HasValue )
                {
                    var bookExists = await _unitOfWork.IslamicBook.ExistsAsync(b => b.Id == dto.BookId.Value);
                    if (!bookExists)
                        return Result<HadithResponseDto>.BadRequest("The book does not exist");

                    newBookId = dto.BookId.Value;
                }
                else if (dto.BookId == null && hadith.BookId.HasValue)
                {
                    newBookId = null;
                }

                if (dto.SectionId.HasValue)
                {
                    var targetBookId = newBookId ?? hadith.BookId;

                    if (!targetBookId.HasValue)
                        return Result<HadithResponseDto>.BadRequest("Cannot specify a section without a book");

                    var sectionExists = await _unitOfWork.IslamicBookSection
                        .ExistsAsync(s => s.Id == dto.SectionId.Value && s.BookId == targetBookId.Value);

                    if (!sectionExists)
                        return Result<HadithResponseDto>.BadRequest("The specified section does not belong to the selected book");

                    newSectionId = dto.SectionId.Value;
                }
                else if (dto.SectionId == null && hadith.SectionId.HasValue)
                {
                    newSectionId = null;
                }
               
                hadith.BookId = newBookId;
                hadith.SectionId = newSectionId;

                if (!string.IsNullOrEmpty(dto.Reference)) hadith.Reference = dto.Reference;
                if (!string.IsNullOrEmpty(dto.Text)) hadith.Text = dto.Text;
                if (dto.Grade != 0) hadith.Grade = dto.Grade;
                if (!string.IsNullOrEmpty(dto.HadithNumber)) hadith.HadithNumber = dto.HadithNumber;

                if (dto.Explanations != null)
                {
                    if (hadith.Explanations?.Any() == true)
                    {
                        foreach (var oldExp in hadith.Explanations.ToList())
                        {
                            await _hadithExplanationRepository.DeleteAsync(oldExp);
                        }
                        hadith.Explanations.Clear();
                    }

                    foreach (var expDto in dto.Explanations)
                    {
                        hadith.Explanations.Add(new HadithExplanation
                        {
                            Scholar = expDto.Scholar,
                            Explanation = expDto.Explanation,
                            HadithId = hadith.Id,
                            CreateAt = DateTime.UtcNow,
                        });
                    }
                }

                hadith.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.CommitTransactionAsync();

                var updated = await _hadithRepository.GetByIdWithDetailsAsync(id);
                return Result<HadithResponseDto>.Success(updated!.ToResponseDto());

            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();

                return Result<HadithResponseDto>.BadRequest("internal Error");

            }
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var hadith = await _hadithRepository.GetByIdAsync(id);
            if (hadith == null)
                return Result.NotFound("Hadith not found");

            hadith.IsDeleted = true;
            hadith.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}