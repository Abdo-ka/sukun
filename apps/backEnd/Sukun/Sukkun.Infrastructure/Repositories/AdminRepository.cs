using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context, ILogger<AdminRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<Admin?> GetByEmailAsync(string email)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(a => a.Email == email && !a.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting admin by email: {Email}", email);
                throw;
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            try
            {
                var query = _dbSet.Where(a => a.Email == email && !a.IsDeleted);
                if (excludeId.HasValue)
                    query = query.Where(a => a.Id != excludeId.Value);

                return !await query.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking admin email uniqueness: {Email}", email);
                throw;
            }
        }
    }
}
