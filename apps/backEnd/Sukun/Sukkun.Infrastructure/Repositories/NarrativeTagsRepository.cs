using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class NarrativeTagsRepository : Repository<NarrativeTags>, INarrativeTagsRepository
    {
        public NarrativeTagsRepository(ApplicationDbContext context, ILogger<Repository<NarrativeTags>> logger)
            : base(context, logger)
        {
        }

    }
}
