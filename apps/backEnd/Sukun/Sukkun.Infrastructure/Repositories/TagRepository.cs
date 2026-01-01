using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context, ILogger<Repository<Tag>> logger)
            : base(context, logger)
        {
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(t => !t.IsDeleted && t.Name == name);
        }
    }}
