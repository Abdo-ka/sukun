using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Helping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Infrastructure.InfrastructureBases
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByIdWithIncludesAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<Result<T>> AddAsync(T entity);
        Task<Result<bool>> AddRangeAsync(IEnumerable<T> entities);
        Task<Result<T>> UpdateAsync(T entity);
        public IQueryable<T> AsQueryable();
        public IQueryable<T> AsQueryableNoTracking();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<Result<bool>> DeleteAsync(T entity, bool softDelete = true);
        Task<Result<bool>> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
