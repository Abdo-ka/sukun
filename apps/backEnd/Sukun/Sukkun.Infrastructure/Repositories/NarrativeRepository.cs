using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class NarrativeRepository : Repository<Narrative>, INarrativeRepository
    {
        public NarrativeRepository(ApplicationDbContext context, ILogger<NarrativeRepository> logger)
            : base(context, logger)
        {
        }
        public async Task<Narrative?> GetByIdWithDetailsAsync(Guid id , ContentType? type = null, Guid? tagId = null, Guid? categoryId = null)
        {
            var query = AsQueryableNoTracking().Where(n => n.Id == id);
           
            if (type.HasValue)
                query = query.Where(x => x.Type == type.Value);

            query = query.Include(n => n.NarrativeTags.Where(s => !s.IsDeleted))
                         .ThenInclude(nt => nt.Tag);

            if (tagId.HasValue)
                query = query.Where(x => x.NarrativeTags.Any(nt => nt.TagId == tagId && !nt.IsDeleted));

            query = query.Include(n => n.NarrativeCategories.Where(s => !s.IsDeleted))
                            .ThenInclude(nc => nc.Category);

            if (categoryId.HasValue)
                query = query.Where(x => x.NarrativeCategories.Any(nt => nt.CategoryId == categoryId && !nt.IsDeleted));

            return await query
                 .Include(n => n.Sections.Where(s => !s.IsDeleted).OrderBy(s => s.DisplayOrder))
                 .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Narrative>> GetAllWithDetailsAsync(ContentType? type = null , Guid? tagId = null, Guid? categoryId = null)
        {
            var query = AsQueryableNoTracking();
            if (type.HasValue)
                query = query.Where(x => x.Type == type.Value);

            query = query.Include(n => n.NarrativeTags)
                         .ThenInclude(nt => nt.Tag);    

            if (tagId.HasValue)
                query = query.Where(x => x.NarrativeTags.Any(nt=>nt.TagId == tagId));

            query = query.Include(n => n.NarrativeCategories)
                     .ThenInclude(nc => nc.Category);

            if (categoryId.HasValue)
                query = query.Where(x => x.NarrativeCategories.Any(nt => nt.CategoryId == categoryId));

            return await query
                 .Include(n => n.Sections.OrderBy(s => s.DisplayOrder))
                 .ToListAsync();
                
        }

        public async Task<IEnumerable<Narrative>> GetFeaturedAsync(int count = 10)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(n => !n.IsDeleted && n.IsFeatured)
                .Include(n => n.Sections)
                .OrderByDescending(n => n.ViewCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Narrative>> GetByCategoryAsync(Guid categoryId)
        {
            return await _context.NarrativeCategories
                .AsNoTracking()
                .Where(nc => nc.CategoryId == categoryId)
                .OrderBy(nc => nc.DisplayOrder)
                .Select(nc => nc.Narrative)
                .Where(n => !n.IsDeleted)
                .Include(n => n.Sections)
                .ToListAsync();
        }

        public async Task<IEnumerable<Narrative>> GetByTagAsync(Guid tagId)
        {

            return await _context.NarrativeTags
                        .AsNoTracking()
                        .Where(nt => nt.TagId == tagId && !nt.IsDeleted)
                        .Select(nt => nt.Narrative)
                        .Where(n => !n.IsDeleted)
                        .Include(n => n.Sections)
                        .ToListAsync();
        }
        public async Task<IEnumerable<Narrative>> GetByTypeAsync(ContentType type)
        {
            return await AsQueryable()
                .Where(x => x.Type == type && !x.IsDeleted)
                .Include(n => n.Sections)
                .ToListAsync();
        }
        public async Task<Narrative?> GetByTagAndTypeAsync(Guid tagId, ContentType? type = null)
        {
            var query = AsQueryable();
            if (type is not null)
                query = query.Where(x => x.Type == type);

            return await _dbSet
                .Include(n => n.NarrativeTags)
                    .ThenInclude(n => n.Tag)
                .FirstOrDefaultAsync(n => !n.IsDeleted && n.NarrativeTags.Any(t => t.TagId  == tagId));
        }

    }
}
