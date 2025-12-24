using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface ITasbihRepository : IRepository<Tasbih>
    {
        Task<IEnumerable<Tasbih>> GetAllOrderedAsync();
        Task<Tasbih?> GetRandomAsync();
    }

}
