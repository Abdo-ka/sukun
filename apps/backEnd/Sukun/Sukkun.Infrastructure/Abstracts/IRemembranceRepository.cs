using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IRemembranceRepository : IRepository<Remembrance>
    {
        Task<IEnumerable<Remembrance>> GetByCategoryAsync(Guid categoryId);
        Task<Remembrance?> GetByIdWithDetailsAsync(Guid id); // مع Categories و Contents
        Task<Remembrance?> GetRandomAsync();
    }
}
