using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public async Task<IEnumerable<Remembrance>> GetByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Where(r => !r.IsDeleted && r.RemembranceCategoryLinks.Any(c =>c.RemembranceCategoryId == categoryId))
                .Include(r => r.Contents.Where(c => !c.IsDeleted))
                .Include(r => r.RemembranceCategoryLinks.Where(c => !c.IsDeleted))
                .ThenInclude(r=> r.RemembranceCategory)
                .ToListAsync();
        }

        public async Task<Remembrance?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(r => r.RemembranceCategoryLinks.Where(c => !c.IsDeleted))
                .ThenInclude(r => r.RemembranceCategory)
                .Include(r => r.Contents.Where(c => !c.IsDeleted))
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<Remembrance?> GetRandomAsync()
        {
            try
            {
                var count = await _dbSet.CountAsync();
                if (count == 0)
                    return null;

                var random = new Random();
                var skip = random.Next(0, count);

                return await _dbSet.AsQueryable()
                    .Skip(skip)
                    .Include(r => r.Contents.Where(c => !c.IsDeleted))
                    .Include(r => r.RemembranceCategoryLinks.Where(c => !c.IsDeleted)).
                    ThenInclude(r => r.RemembranceCategory)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting random Remembrance item");
                throw;
            }
        }
    }
}
