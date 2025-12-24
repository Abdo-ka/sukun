using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class RemembranceContentRepository : Repository<RemembranceContent>, IRemembranceContentRepository
    {
        public RemembranceContentRepository(ApplicationDbContext context, ILogger<Repository<RemembranceContent>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<RemembranceContent>> GetByRemembranceAsync(Guid remembranceId)
        {
            return await _dbSet
                .Where(c => !c.IsDeleted && c.RemembranceId == remembranceId)
                .ToListAsync();
        }
    }
}
