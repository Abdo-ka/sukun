using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IDuaItemRepository : IRepository<DuaItem>
    {
        Task<IEnumerable<DuaItem>> GetByCategoryAsync(Guid categoryId);
        Task<DuaItem?> GetRandomAsync();
    }
}
