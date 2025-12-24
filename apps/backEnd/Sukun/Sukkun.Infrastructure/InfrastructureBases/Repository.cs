using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Helping;
using Sukun.Infrastructure.Context;
using System.Linq.Expressions;
using System.Threading;

namespace Sukun.Infrastructure.InfrastructureBases
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<Repository<T>> _logger;

        public Repository(ApplicationDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet
            .Where(e => e.Id == id && !e.IsDeleted)
            .FirstOrDefaultAsync();
        }
        public async Task<T?> GetByIdWithIncludesAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(e => !e.IsDeleted).Where(predicate).ToListAsync();
        }
        public async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .Where(e => !e.IsDeleted)
                .FirstOrDefaultAsync(predicate);
        }
        public async Task<Result<T>> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return Result<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure($"Error adding entity: {ex.Message}");
            }
        }

        public async Task<Result<T>> UpdateAsync(T entity)
        {
            try
            {
                entity.UpdatedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                return Result<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure($"Error updating entity: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteAsync(T entity, bool softDelete = true)
        {
            try
            {           
                if (softDelete)
                {
                    entity.IsDeleted = true;
                    entity.UpdatedAt = DateTime.UtcNow;
                    _dbSet.Update(entity);
                }
                else
                {
                    _dbSet.Remove(entity);
                }
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting entity: {ex.Message}");
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(e => !e.IsDeleted).AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = _dbSet.Where(e => !e.IsDeleted);

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable().Where(e => !e.IsDeleted);
        }
        public IQueryable<T> AsQueryableNoTracking()
        {
            return _dbSet.AsQueryable().Where(e => !e.IsDeleted).AsNoTracking();
        }
  
        public virtual async Task<Result<int>> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                var result = await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(result);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while deleting multiple entities");
                return Result<int>.Failure(dbEx.InnerException?.Message ?? dbEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting multiple entities");
                return Result<int>.Failure(ex.Message);
            }
        }
        public virtual async Task<Result<int>> DeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            try
            {
                var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
                if (!entities.Any())
                    return Result<int>.Success(0);

                _dbSet.RemoveRange(entities);
                var result = await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(result);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while deleting multiple entities by predicate");
                return Result<int>.Failure(dbEx.InnerException?.Message ?? dbEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting multiple entities by predicate");
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
