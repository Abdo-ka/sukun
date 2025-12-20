using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Linq.Expressions;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserWithDetailsAsync(Guid userId);
        Task<bool> UserExistsAsync(Guid userId);
    }

}
