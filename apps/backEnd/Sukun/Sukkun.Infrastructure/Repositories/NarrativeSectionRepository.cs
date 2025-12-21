using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class NarrativeSectionRepository : Repository<NarrativeSection>, INarrativeSectionRepository
    {
        public NarrativeSectionRepository(ApplicationDbContext context, ILogger<Repository<NarrativeSection>> logger)
            : base(context, logger)
        {
        }

        public async Task<NarrativeSection?> GetByIdWithNarrativeAsync(Guid sectionId)
        {
            return await _dbSet
                .Include(s => s.Narrative)
                .FirstOrDefaultAsync(s => s.Id == sectionId && !s.IsDeleted);
        }
    }
}
