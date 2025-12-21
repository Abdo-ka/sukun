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

        public async Task<IEnumerable<Narrative>> GetAllWithDetailsAsync()
        {
            return await AsQueryable()
                .Include(n => n.Sections.OrderBy(s => s.DisplayOrder))
                .Include(n => n.Children)
                    .ThenInclude(c => c.Sections.OrderBy(s => s.DisplayOrder))
                .ToListAsync();
        }

        public async Task<Narrative?> GetByIdWithFullDetailsAsync(Guid id)
        {
            return await AsQueryable()
                .Include(n => n.Sections.OrderBy(s => s.DisplayOrder))
                .Include(n => n.Children)
                    .ThenInclude(c => c.Sections.OrderBy(s => s.DisplayOrder))
                .Include(n => n.Parent)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Narrative>> GetByTypeAsync(ContentType type)
        {
            return await AsQueryable()
                .Where(x => x.Type == type && !x.IsDeleted)
                .Include(n => n.Sections.OrderBy(s => s.DisplayOrder))
                .ToListAsync();
        }

        public async Task<IEnumerable<Narrative>> GetFeaturedAsync()
        {
            return await AsQueryable()
                .Where(x => x.IsFeatured && !x.IsDeleted)
                .Include(n => n.Sections.OrderBy(s => s.DisplayOrder))
                .ToListAsync();
        }

        public async Task<IEnumerable<Narrative>> GetChildrenAsync(Guid parentId)
        {
            return await AsQueryable()
                .Where(x => x.ParentId == parentId && !x.IsDeleted)
                .Include(n => n.Sections.OrderBy(s => s.DisplayOrder))
                .ToListAsync();
        }

        public async Task<IEnumerable<Narrative>> GetRootNarrativesAsync()
        {

            return await AsQueryable()
                .Where(x => x.ParentId == null && !x.IsDeleted)
                .Include(n => n.Sections.OrderBy(s => s.DisplayOrder))
                .ToListAsync();
        }
        public async Task<Narrative?> GetByTagAsync(NarrativeTag tag)
        {
            return await _dbSet
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => !n.IsDeleted && n.Tags.Any(t => t.TagType == tag));
        }

        public async Task<IEnumerable<Narrative>> GetChildrenOfTaggedSectionAsync(NarrativeTag tag)
        {
            var section = await GetByTagAsync(tag);
            if (section == null) return Enumerable.Empty<Narrative>();

            return await _dbSet
                .Where(n => !n.IsDeleted && n.ParentId == section.Id)
                .ToListAsync();
        }
    }
}
