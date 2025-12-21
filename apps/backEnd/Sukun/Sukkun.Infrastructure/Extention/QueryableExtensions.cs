using Microsoft.EntityFrameworkCore;
using Sukun.Application.Dtos.User_Entity.Request;
using System.Linq.Expressions;
using System.Threading;

namespace Sukun.Domin.Helping
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResponseDto<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize) where T : class
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalCount = await query.AsNoTracking().CountAsync();
            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResponseDto<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < totalPages,
                Items = items
            };
        }
    }
}
