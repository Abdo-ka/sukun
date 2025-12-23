using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;
using Sukun.Infrastructure.Repositories;

namespace Sukun.Infrastructure.Abstracts
{
    public class DuaCategoryRepository : Repository<DuaCategory>, IDuaCategoryRepository
    {
        public DuaCategoryRepository(ApplicationDbContext context, ILogger<Repository<DuaCategory>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<DuaCategory>> GetAllWithDuasCountAsync()
        {
            return await _dbSet
                .Where(c => !c.IsDeleted)
                .Include(c=>c.Duas)
                .OrderBy(c => c.CreateAt)
                .ToListAsync();
        }
        public async Task<DuaCategory?> GetRandomAsync()
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
