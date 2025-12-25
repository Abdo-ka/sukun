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
        private readonly ILogger<HadithService> _logger;

        public HadithService(
            IUnitOfWork unitOfWork,
            IHadithCategoryRepository categoryRepository,
            IHadithRepository hadithRepository,
            ILogger<HadithService> logger)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _hadithRepository = hadithRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<HadithCategoryResponseDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllWithHadithsCountAsync();
            var dtos = categories.Select(c => c.ToResponseDto());
            return Result<IEnumerable<HadithCategoryResponseDto>>.Success(dtos);
        }

        public async Task<Result<PagedResponseDto<HadithListResponseDto>>> GetPagedAsync(PagedRequestDto request)
        {
            if (request.PageNumber < 1) request.PageNumber = 1;
            if (request.PageSize < 1 || request.PageSize > 100) request.PageSize = 20;

            var query = _hadithRepository.AsQueryable();
            query =  query.Include(x => x.Book)
                 .Include(x => x.Category)
                 .Include(x => x.Section);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(h =>
                    h.Text.ToLower().Contains(term) ||
                    h.Reference.ToLower().Contains(term) ||
                    (h.Book.Name != null && h.Book.Name.ToLower().Contains(term)));
            }

            query = query.OrderBy(h => h.HadithNumber);

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

        public async Task<Result<PagedResponseDto<HadithListResponseDto>>> SearchAsync(string query, PagedRequestDto? paging = null)
        {
            paging ??= new PagedRequestDto { PageNumber = 1, PageSize = 20 };

            var request = new PagedRequestDto
            {
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize,
                SearchTerm = query
            };

            return await GetPagedAsync(request);
        }

        // ====================== Admin CRUD ======================

        public async Task<Result<HadithResponseDto>> CreateAsync(HadithCreateDto dto)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(c => c.Id == dto.CategoryId);
            if (!categoryExists)
                return Result<HadithResponseDto>.BadRequest("Invalid CategoryId");

            var hadith = dto.ToEntity();

            var addResult = await _hadithRepository.AddAsync(hadith);
            if (!addResult.IsSuccess)
                return Result<HadithResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            var created = await _hadithRepository.GetByIdWithDetailsAsync(hadith.Id);
            return Result<HadithResponseDto>.Success(created!.ToResponseDto());
        }

        public async Task<Result<HadithResponseDto>> UpdateAsync(Guid id, HadithUpdateDto dto)
        {
            var hadith = await _hadithRepository.GetByIdWithDetailsAsync(id);
            if (hadith == null)
                return Result<HadithResponseDto>.NotFound("Hadith not found");

            if (dto.CategoryId != Guid.Empty && dto.CategoryId != hadith.CategoryId)
            {
                var categoryExists = await _categoryRepository.ExistsAsync(c => c.Id == dto.CategoryId);
                if (!categoryExists)
                    return Result<HadithResponseDto>.BadRequest("Invalid CategoryId");
                hadith.CategoryId = dto.CategoryId;
            }

            if (!string.IsNullOrEmpty(dto.Reference)) hadith.Reference = dto.Reference;
            if (!string.IsNullOrEmpty(dto.Text)) hadith.Text = dto.Text;
            if (dto.Grade != 0) hadith.Grade = dto.Grade;
            if (!string.IsNullOrEmpty(dto.HadithNumber)) hadith.HadithNumber = dto.HadithNumber;

            // تحديث الشروح (بسيط: حذف القديمة وإضافة الجديدة)
            if (dto.Explanations != null)
            {
                hadith.Explanations.Clear();
                foreach (var expDto in dto.Explanations)
                {
                    hadith.Explanations.Add(new HadithExplanation
                    {
                        Id = Guid.NewGuid(),
                        Scholar = expDto.Scholar,
                        Explanation = expDto.Explanation,
                        HadithId = hadith.Id
                    });
                }
            }

            hadith.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            var updated = await _hadithRepository.GetByIdWithDetailsAsync(id);
            return Result<HadithResponseDto>.Success(updated!.ToResponseDto());
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