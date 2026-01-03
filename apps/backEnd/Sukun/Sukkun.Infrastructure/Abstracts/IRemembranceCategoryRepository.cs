using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IRemembranceCategoryRepository : IRepository<RemembranceCategory>
    {
        Task<IEnumerable<RemembranceCategory>> GetAllWithRemembrancesAsync();
    }
}
