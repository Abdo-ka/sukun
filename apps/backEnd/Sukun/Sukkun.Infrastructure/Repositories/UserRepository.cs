using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;
using System.Linq.Expressions;

namespace Sukun.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
            : base(context, logger)
        {
        }
        public async Task<User?> GetUserWithDetailsAsync(Guid userId)
        {
            try
            {
                return await _dbSet
                    .Include(u => u.City)
                    .Include(u => u.Devices)
                    .Include(u => u.Bookmarks)
                        .ThenInclude(b => b.Verse)
                        .ThenInclude(v => v.Surah)
                    .FirstOrDefaultAsync(u => u.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting anonymous user with details: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            try
            {
                return await _dbSet.AnyAsync(u => u.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if anonymous user exists: {UserId}", userId);
                throw;
            }
        }
    }


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
