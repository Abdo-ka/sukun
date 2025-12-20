using Microsoft.EntityFrameworkCore;
using Sukun.Application.Dtos.User_Entity.Request;
using System.Linq.Expressions;
using System.Threading;

namespace Sukun.Domin.Helping
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResponseDto<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
          where T : class
        {
            if (query == null)
            {
                throw new Exception("Empty");
            }

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            int count = await query.AsNoTracking().CountAsync();
            if (count == 0)
                return new PagedResponseDto<T>();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponseDto<T>(){};
        }
        public static  IQueryable<T> ApplyPaginatedAsync<T>(this IQueryable<T> query,
        int pageNumber,
        int pageSize)
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}
