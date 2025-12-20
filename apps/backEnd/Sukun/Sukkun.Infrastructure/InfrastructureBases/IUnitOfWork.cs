using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;

namespace Sukun.Infrastructure.InfrastructureBases
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        // Specific Repositories
        public IAdminRepository Admins { get; }
        public ICityRepository Cities { get; }
        public IQuranRepository Quran { get; }
        // Save changes
       public Task<int> CompleteAsync();
       public Task<bool> SaveEntitiesAsync();
       // Transaction support
       public Task BeginTransactionAsync();
       public Task CommitTransactionAsync();
       public Task RollbackTransactionAsync();
    }
}
