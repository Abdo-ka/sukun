using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IHadithCategoryRepository : IRepository<HadithCategory>
    {
        Task<IEnumerable<HadithCategory>> GetAllWithHadithsCountAsync();
        Task<int> GetHadithsCountAsync(Guid categoryId);
    }
}
