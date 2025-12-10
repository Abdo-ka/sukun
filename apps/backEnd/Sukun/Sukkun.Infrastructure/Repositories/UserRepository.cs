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

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email: {Email}", email);
                throw;
            }
        }

        public async Task<User?> GetByEmailWithIncludesAsync(string email, params Expression<Func<User, object>>[] includes)
        {
            try
            {
                var query = _dbSet.AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email with includes: {Email}", email);
                throw;
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null)
        {
            try
            {
                var query = _dbSet.Where(u => u.Email == email);

                if (excludeUserId.HasValue)
                {
                    query = query.Where(u => u.Id != excludeUserId.Value);
                }

                return !await query.AnyAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking email uniqueness: {Email}", email);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            try
            {
                return await _dbSet
                    .Where(u => u.IsActive)
                    .OrderBy(u => u.FullName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active users");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
        {
            try
            {
                return await _dbSet
                    .Where(u => u.Role == role && u.IsActive)
                    .OrderBy(u => u.FullName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users by role: {Role}", role);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersByCityAsync(Guid cityID)
        {
            try
            {
                return await _dbSet
                    .Where(u => u.CityId == cityID && u.IsActive)
                    .Include(u => u.City)
                    .OrderBy(u => u.FullName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users by city: {cityID}", cityID);
                throw;
            }
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
                _logger.LogError(ex, "Error getting user with details: {UserId}", userId);
                throw;
            }
        }

        public async Task<Result<User>> UpdateLastLoginAsync(Guid userId)
        {
            try
            {
                var user = await GetByIdAsync(userId);
                if (user == null)
                    return Result<User>.Failure($"User with ID {userId} not found");

                user.LastLoginDate = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(user);
                await _context.SaveChangesAsync();

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last login for user: {UserId}", userId);
                return Result<User>.Failure(ex.Message);
            }
        }

        public async Task<Result<User>> DeactivateUserAsync(Guid userId)
        {
            try
            {
                var user = await GetByIdAsync(userId);
                if (user == null)
                    return Result<User>.Failure($"User with ID {userId} not found");

                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(user);
                await _context.SaveChangesAsync();

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating user: {UserId}", userId);
                return Result<User>.Failure(ex.Message);
            }
        }

        public async Task<Result<User>> ActivateUserAsync(Guid userId)
        {
            try
            {
                var user = await GetByIdAsync(userId);
                if (user == null)
                    return Result<User>.Failure($"User with ID {userId} not found");

                user.IsActive = true;
                user.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(user);
                await _context.SaveChangesAsync();

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating user: {UserId}", userId);
                return Result<User>.Failure(ex.Message);
            }
        }

        public async Task<int> CountByRoleAsync(UserRole role)
        {
            try
            {
                return await _dbSet
                    .Where(u => u.Role == role && u.IsActive)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting users by role: {Role}", role);
                throw;
            }
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllAsync();

                var normalizedSearchTerm = searchTerm.ToLower();

                return await _dbSet.Include(x => x.City)
                    .Where(u => u.IsActive &&
                          (u.FullName != null && u.FullName.ToLower().Contains(normalizedSearchTerm) ||
                           u.Email != null && u.Email.ToLower().Contains(normalizedSearchTerm) ||
                           u.City.Name != null && u.City.Name.ToLower().Contains(normalizedSearchTerm)))
                    .OrderBy(u => u.FullName)
                    .Take(50) // Limit results
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching users with term: {SearchTerm}", searchTerm);
                throw;
            }
        }
    }
}
