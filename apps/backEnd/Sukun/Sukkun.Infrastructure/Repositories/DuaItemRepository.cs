using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;
using Sukun.Infrastructure.Repositories;

namespace Sukun.Infrastructure.Abstracts
{
    public class DuaItemRepository : Repository<DuaItem>, IDuaItemRepository
    {
        public DuaItemRepository(ApplicationDbContext context, ILogger<Repository<DuaItem>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<DuaItem>> GetByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Where(d => !d.IsDeleted && d.CategoryId == categoryId)
                .OrderBy(d => d.DisplayOrder)
                .ToListAsync();
        }
        public async Task<DuaItem?> GetRandomAsync()
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
                    .Include(d => d.Category)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting random Dua item");
                throw;
            }
        }
    }
}
