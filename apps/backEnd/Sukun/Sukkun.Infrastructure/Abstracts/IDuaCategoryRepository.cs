using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IDuaCategoryRepository : IRepository<DuaCategory>
    {
        Task<IEnumerable<DuaCategory>> GetAllWithDuasCountAsync();
        Task<DuaCategory?> GetRandomAsync();
    }
}
