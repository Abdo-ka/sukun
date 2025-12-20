using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.Repositories;

namespace Sukun.Infrastructure.InfrastructureBases
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        private IDbContextTransaction? _currentTransaction;
        private readonly ILogger<UnitOfWork> _logger;
        private bool _disposed = false;

        // Specific Repositories
        public IAdminRepository Admins { get; }
        public ICityRepository Cities { get; }
        public IQuranRepository Quran { get; }

        
        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger,
            ICityRepository cityRepository,
            IQuranRepository quranRepository,
            IAdminRepository adminRepository
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repositories = new Dictionary<Type, object>();

            // Initialize specific repositories
            Admins = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            Cities = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            Quran = quranRepository ?? throw new ArgumentNullException(nameof(quranRepository));
        }
        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                // جلب ILogger<Repository<T>> من DI (يجب أن يكون UnitOfWork مسجل كـ Scoped)
                var repositoryType = typeof(Repository<>);

                var logger = (ILogger<Repository<T>>)_context.GetService(typeof(ILogger<Repository<T>>))
                         ?? throw new InvalidOperationException($"Logger for Repository<{type.Name}> not found.");
                var repositoryInstance = Activator.CreateInstance(
                             repositoryType.MakeGenericType(typeof(T)), _context, logger);

                _repositories[type] = repositoryInstance!;
            }

            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> SaveEntitiesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    foreach (var repository in _repositories.Values)
                    {
                        if (repository is IDisposable disposable)
                            disposable.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (!_disposed)
            {
                await _context.DisposeAsync();
                foreach (var repository in _repositories.Values)
                {
                    if (repository is IAsyncDisposable asyncDisposable)
                        await asyncDisposable.DisposeAsync();
                    else if (repository is IDisposable disposable)
                        disposable.Dispose();
                }
                _disposed = true;
            }
        }

        #region Transaction Support
        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                _logger.LogWarning("Transaction already exists");
                return;
            }

            _currentTransaction = await _context.Database.BeginTransactionAsync();
            _logger.LogInformation("Transaction started");
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit");
            }

            try
            {
                await _context.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
                _logger.LogInformation("Transaction committed successfully");
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("No active transaction to rollback");
            }

            try
            {
                await _currentTransaction.RollbackAsync();
                _logger.LogInformation("Transaction rolled back");
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }
        #endregion
    }
}
