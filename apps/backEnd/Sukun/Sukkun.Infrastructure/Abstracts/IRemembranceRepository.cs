using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IRemembranceRepository : IRepository<Remembrance>
    {
        Task<IEnumerable<Remembrance>> GetWithCategoriesAsync();
        Task<Remembrance?> GetByIdWithFullDetailsAsync(Guid id);
        Task<IEnumerable<Remembrance>> SearchAsync(string searchTerm);
        Task<IEnumerable<Remembrance>> GetAllWithFullDetailsAsync();
        Task<IEnumerable<Remembrance>> GetByCategoryWithFullDetailsAsync(Guid categoryId);
        Task<Remembrance?> GetRandomWithFullDetailsAsync();
        Task<(IEnumerable<Remembrance> Items, int TotalCount)> GetPagedWithFullDetailsAsync(
            PagedRequestDto request, Guid? categoryId = null);

    }
}
