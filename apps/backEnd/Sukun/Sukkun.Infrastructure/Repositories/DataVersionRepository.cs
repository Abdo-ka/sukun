using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class DataVersionRepository : Repository<DataVersion>, IDataVersionRepository
    {
        public DataVersionRepository(ApplicationDbContext context, ILogger<Repository<DataVersion>> logger)
            : base(context, logger)
        {
        }

        public async Task<Dictionary<string, long>> GetAllVersionsDictionaryAsync()
        {
            return await _dbSet
                .Where(v => !v.IsDeleted)
                .ToDictionaryAsync(v => v.TableName, v => v.Version);
        }
    }
}
