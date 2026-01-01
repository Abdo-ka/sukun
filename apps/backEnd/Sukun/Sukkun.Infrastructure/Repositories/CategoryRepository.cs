using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context, ILogger<Repository<Category>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Category>> GetMainSectionsAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.IsMainSection )
                .OrderBy(c => c.Order)
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetRootAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.ParentId == null && c.IsMainSection)
                .OrderBy(c => c.Order)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetWithChildrenAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => !c.IsDeleted && c.ParentId == null && c.IsMainSection)
                .Include(c => c.Children.Where(child => !child.IsDeleted))
                    .ThenInclude(grand => grand.Children.Where(g => !g.IsDeleted))
                .OrderBy(c => c.Order)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdWithChildrenAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.Children.Where(child => !child.IsDeleted))
                    .ThenInclude(grand => grand.Children.Where(g => !g.IsDeleted))
                .FirstOrDefaultAsync();
        }

        public async Task<Category?> GetByIdWithNarrativesAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.Id == id && !c.IsDeleted)
                .Include(c => c.NarrativeCategories)
                    .ThenInclude(nc => nc.Narrative)
                        .ThenInclude(n => n.Sections)
                .Include(c => c.NarrativeCategories)
                    .ThenInclude(nc => nc.Narrative)
                        .ThenInclude(n => n.NarrativeTags)
                            .ThenInclude(nt => nt.Tag)
                .FirstOrDefaultAsync();
        }
    }
}
