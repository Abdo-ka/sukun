using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class RemembranceRepository : Repository<Remembrance>, IRemembranceRepository
    {
        public RemembranceRepository(ApplicationDbContext context, ILogger<Repository<Remembrance>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Remembrance>> GetWithCategoriesAsync()
        {
            return await _dbSet
                .Where(e => !e.IsDeleted)
                .Include(r => r.RemembranceCategoryLinks.Where(c => !c.IsDeleted))
                    .ThenInclude(l => l.RemembranceCategory)
                .ToListAsync();
        }

        public async Task<Remembrance?> GetByIdWithFullDetailsAsync(Guid id)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Where(e => e.Id == id && !e.IsDeleted)
                .Include(r => r.Contents.Where(c => !c.IsDeleted))
                .Include(r => r.RemembranceCategoryLinks.Where(c => !c.IsDeleted))
                    .ThenInclude(l => l.RemembranceCategory)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Remembrance>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(e => !e.IsDeleted &&
                           (e.Title.Contains(searchTerm) ||
                            e.Contents.Any(c => c.CustomContent != null && c.CustomContent.Contains(searchTerm))))
                .Include(r => r.Contents.Where(c => !c.IsDeleted))
                .ToListAsync();
        }

        public async Task<IEnumerable<Remembrance>> GetAllWithFullDetailsAsync()
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Where(r => !r.IsDeleted)
                .Include(r => r.RemembranceCategoryLinks)
                    .ThenInclude(l => l.RemembranceCategory)
                .Include(r => r.Contents)
                .OrderBy(r => r.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Remembrance>> GetByCategoryWithFullDetailsAsync(Guid categoryId)
        {
            return await _dbSet.AsQueryable().AsNoTracking()
                .Where(r => !r.IsDeleted &&
                            r.RemembranceCategoryLinks.Any(l => l.RemembranceCategoryId == categoryId))
                .Include(r => r.RemembranceCategoryLinks)
                    .ThenInclude(l => l.RemembranceCategory)
                .Include(r => r.Contents)
                .ToListAsync();
        }

        public async Task<Remembrance?> GetRandomWithFullDetailsAsync()
        {
            var count = await _dbSet.CountAsync(r => !r.IsDeleted);
            if (count == 0) return null;

            var random = new Random();
            var skip = random.Next(0, count);

            return await _dbSet.AsQueryable().AsNoTracking()
                .Where(r => !r.IsDeleted)
                .Skip(skip)
                .Include(r => r.RemembranceCategoryLinks)
                    .ThenInclude(l => l.RemembranceCategory)
                .Include(r => r.Contents)
                .FirstOrDefaultAsync();
        }

        public async Task<(IEnumerable<Remembrance> Items, int TotalCount)> GetPagedWithFullDetailsAsync(
            PagedRequestDto request, Guid? categoryId = null)
        {
            if (request.PageNumber < 1) request.PageNumber = 1;
            if (request.PageSize < 1 || request.PageSize > 100) request.PageSize = 20;

            var query = _dbSet.AsQueryable().AsNoTracking()
                .Where(r => !r.IsDeleted);

            if (categoryId.HasValue)
            {
                query = query.Where(r => r.RemembranceCategoryLinks
                    .Any(l => l.RemembranceCategoryId == categoryId.Value));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(r =>
                    r.Title.ToLower().Contains(term) ||
                    r.Contents.Any(c => !c.IsDeleted &&
                        (c.CustomContent != null && c.CustomContent.ToLower().Contains(term))));
            }

            query = query
                .Include(r => r.RemembranceCategoryLinks)
                    .ThenInclude(l => l.RemembranceCategory)
                .Include(r => r.Contents)
                .OrderBy(r => r.Title);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
