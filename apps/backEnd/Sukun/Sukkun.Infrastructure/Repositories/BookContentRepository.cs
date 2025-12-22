using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class BookContentRepository : Repository<BookContent>, IBookContentRepository
    {
        public BookContentRepository(ApplicationDbContext context, ILogger<Repository<BookContent>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<BookContent>> GetBySectionAsync(Guid sectionId)
        {
            return await _dbSet
                .Where(c => !c.IsDeleted && c.SectionId == sectionId)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
        }
    }
}
