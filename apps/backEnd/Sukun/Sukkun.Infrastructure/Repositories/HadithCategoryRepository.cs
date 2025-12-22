using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class HadithCategoryRepository : Repository<HadithCategory>, IHadithCategoryRepository
    {
        public HadithCategoryRepository(ApplicationDbContext context, ILogger<Repository<HadithCategory>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<HadithCategory>> GetAllWithHadithsCountAsync()
        {
            return await _dbSet
                .Where(c => !c.IsDeleted)
                .Select(c => new HadithCategory
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Hadiths = c.Hadiths.Where(h => !h.IsDeleted).ToList() // للعدد فقط
                })
                .ToListAsync();
        }
        public async Task<int> GetHadithsCountAsync(Guid categoryId)
        {
            return await _context.Set<Hadith>()
                .CountAsync(h => h.CategoryId == categoryId && !h.IsDeleted);
        }
    }
}



