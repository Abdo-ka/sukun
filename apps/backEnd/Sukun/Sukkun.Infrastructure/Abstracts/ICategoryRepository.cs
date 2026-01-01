using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetRootAsync();
        Task<IEnumerable<Category>> GetMainSectionsAsync();
        Task<IEnumerable<Category>> GetWithChildrenAsync();
        Task<Category?> GetByIdWithNarrativesAsync(Guid id);
        Task<Category?> GetByIdWithChildrenAsync(Guid id);
    }
}
