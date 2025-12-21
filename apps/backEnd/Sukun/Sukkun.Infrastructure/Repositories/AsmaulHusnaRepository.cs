using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class AsmaulHusnaRepository : Repository<AsmaulHusna>, IAsmaulHusnaRepository
    {
        public AsmaulHusnaRepository(ApplicationDbContext context, ILogger<AsmaulHusnaRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<AsmaulHusna?> GetByNumberAsync(int number)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Number == number);
        }

        public async Task<IEnumerable<AsmaulHusna>> GetRandomAsync(int count)
        {
            return await _dbSet
                .OrderBy(r => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }
    }

}
