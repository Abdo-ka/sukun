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
                .Where(h => !h.IsDeleted && h.CategoryId == categoryId)
                .OrderBy(h => h.HadithNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hadith>> GetRandomAsync(int count = 1)
        {
            return await _dbSet
                .Where(h => !h.IsDeleted && h.Grade == HadithGrade.Sahih) // اختياري: فقط الصحيح
                .OrderBy(h => EF.Functions.Random())
                .Take(count)
                .ToListAsync();
        }

        public async Task<Hadith?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(h => h.Category)
                .Include(h => h.Explanations)
                .FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);
        }

        public async Task<IEnumerable<Hadith>> SearchAsync(string query)
        {
            var lowerQuery = query.ToLower();
            return await _dbSet
                .Where(h => !h.IsDeleted &&
                    (h.Text.ToLower().Contains(lowerQuery) ||
                     h.Reference.ToLower().Contains(lowerQuery) ||
                     h.BookName != null && h.BookName.ToLower().Contains(lowerQuery)))
                .Take(50)
                .ToListAsync();
        }
    }
}
