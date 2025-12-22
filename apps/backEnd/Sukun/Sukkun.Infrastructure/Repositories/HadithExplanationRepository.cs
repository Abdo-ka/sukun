using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

public class HadithExplanationRepository : Repository<HadithExplanation>, IHadithExplanationRepository
    {
        public HadithExplanationRepository(ApplicationDbContext context, ILogger<Repository<HadithExplanation>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<HadithExplanation>> GetByHadithAsync(Guid hadithId)
        {
            return await _dbSet
                .Where(e => !e.IsDeleted && e.HadithId == hadithId)
                .OrderBy(e => e.CreateAt)
                .ToListAsync();
        }
    }
