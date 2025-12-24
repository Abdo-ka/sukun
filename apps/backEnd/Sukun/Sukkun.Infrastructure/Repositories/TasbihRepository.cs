using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class TasbihRepository : Repository<Tasbih>, ITasbihRepository
    {
        public TasbihRepository(ApplicationDbContext context, ILogger<Repository<Tasbih>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Tasbih>> GetAllOrderedAsync()
        {
            return await _dbSet
                .Where(t => !t.IsDeleted)
                .OrderBy(t => t.Order)
                .ThenBy(t => t.Title)
                .ToListAsync();
        }

        public async Task<Tasbih?> GetRandomAsync()
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
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting random dua category");
                throw;
            }
        }
    }
}
