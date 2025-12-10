using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Linq.Expressions;

namespace Sukun.Infrastructure.Abstracts
{
    // Specific Repositories
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailWithIncludesAsync(string email, params Expression<Func<User, object>>[] includes);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
        Task<IEnumerable<User>> GetUsersByCityAsync(Guid CityId);
        Task<User?> GetUserWithDetailsAsync(Guid userId);
        Task<Result<User>> UpdateLastLoginAsync(Guid userId);
        Task<Result<User>> DeactivateUserAsync(Guid userId);
        Task<Result<User>> ActivateUserAsync(Guid userId);
        Task<int> CountByRoleAsync(UserRole role);
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);
    }
}
