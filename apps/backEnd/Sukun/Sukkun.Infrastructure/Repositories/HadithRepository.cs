using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class HadithRepository : Repository<Hadith>, IHadithRepository
    {
        public HadithRepository(ApplicationDbContext context, ILogger<Repository<Hadith>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Hadith>> GetByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Include(h => h.Book)
                .Include(h => h.Category)
                .Include(h=>h.Section)
                .Include(h => h.Explanations)
                .Where(h => !h.IsDeleted && h.CategoryId == categoryId)
                .OrderBy(h => h.HadithNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hadith>> GetRandomAsync(int count = 1)
        {

            try
            {
                var countExist = await _dbSet.CountAsync();
                if (countExist == 0)
                    return null;

                var random = new Random();
                var skip = random.Next(0, count);

                return await _dbSet.AsQueryable()
                    .Skip(skip)
                    .Take(count)
                    .Include(h => h.Book)
                    .Include(h => h.Category)
                    .Include(h => h.Explanations)
                    .Include(h => h.Section)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting random Dua item");
                throw;
            }
        }

        public async Task<Hadith?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Where(h => h.Id == id && !h.IsDeleted)
                .Include(h => h.Category)
                .Include(h => h.Explanations)
                .Include(h => h.Book)
                .Include(h => h.Section)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Hadith>> SearchAsync(string query)
        {
            var lowerQuery = query.ToLower();

            return await _dbSet
                .Include(h => h.Book)
                .Where(h => !h.IsDeleted &&
                    (h.Text.ToLower().Contains(lowerQuery) ||
                     h.Reference.ToLower().Contains(lowerQuery) ||
                     h.Book.Name != null && h.Book.Name.ToLower().Contains(lowerQuery)))
                .Take(50)
                .Include(h => h.Category)
                 .Include(h => h.Explanations)
                .Include(h => h.Section)
                .ToListAsync();
        }
    }
}
