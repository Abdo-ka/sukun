using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class NarrativeCategoryRepository : Repository<NarrativeCategory>, INarrativeCategoryRepository
    {
        public NarrativeCategoryRepository(ApplicationDbContext context, ILogger<Repository<NarrativeCategory>> logger)
            : base(context, logger)
        {
        }
    }
}
