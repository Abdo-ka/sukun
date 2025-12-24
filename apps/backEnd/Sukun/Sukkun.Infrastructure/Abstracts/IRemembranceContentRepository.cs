using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IRemembranceContentRepository : IRepository<RemembranceContent>
    {
        Task<IEnumerable<RemembranceContent>> GetByRemembranceAsync(Guid remembranceId);
    }
}
