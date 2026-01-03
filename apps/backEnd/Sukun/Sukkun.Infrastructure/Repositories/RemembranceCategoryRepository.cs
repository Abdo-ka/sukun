using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class RemembranceCategoryRepository : Repository<RemembranceCategory>, IRemembranceCategoryRepository
    {
        public RemembranceCategoryRepository(ApplicationDbContext context, ILogger<Repository<RemembranceCategory>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<RemembranceCategory>> GetAllWithRemembrancesAsync()
        {
            return await _dbSet
                .Where(c => !c.IsDeleted)
                .Include(c => c.RemembranceCategoryLinks)
                .ThenInclude(c => c.Remembrance)
                .ToListAsync();
        }
    }
}
