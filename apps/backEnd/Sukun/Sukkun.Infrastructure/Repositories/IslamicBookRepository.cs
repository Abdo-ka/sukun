using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class IslamicBookRepository : Repository<IslamicBook>, IIslamicBookRepository
    {
        public IslamicBookRepository(ApplicationDbContext context, ILogger<Repository<IslamicBook>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<IslamicBook>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Where(b => !b.IsDeleted)
                .OrderBy(b => b.Order)
                .Include(b => b.Sections.Where(s => !s.IsDeleted))
                .Include(b => b.Hadiths.Where(h => !h.IsDeleted))
                .ToListAsync();
        }

        public async Task<IslamicBook?> GetByIdWithSectionsAsync(Guid id)
        {
            return await _dbSet
                .Include(b => b.Sections.Where(s => !s.IsDeleted))
                .Include(b => b.Hadiths.Where(h => !h.IsDeleted))
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<IEnumerable<IslamicBook>> GetByTypeAsync(BookType type)
        {
            return await _dbSet
                .Where(b => !b.IsDeleted && b.Type == type)
                .Include(b => b.Hadiths.Where(h => !h.IsDeleted))
                .Include(b => b.Sections.Where(s => !s.IsDeleted))
                .OrderBy(b => b.Order)
                .ToListAsync();
        }
    }
}
