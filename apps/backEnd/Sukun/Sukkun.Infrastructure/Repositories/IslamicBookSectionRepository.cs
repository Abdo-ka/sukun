using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class IslamicBookSectionRepository : Repository<IslamicBookSection>, IIslamicBookSectionRepository
    {
        public IslamicBookSectionRepository(ApplicationDbContext context, ILogger<Repository<IslamicBookSection>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<IslamicBookSection>> GetByBookAsync(Guid bookId)
        {
            return await _dbSet
                .Where(s => !s.IsDeleted && s.BookId == bookId)
                .OrderBy(s => s.Order)
                .ToListAsync();
        }
    }
}
