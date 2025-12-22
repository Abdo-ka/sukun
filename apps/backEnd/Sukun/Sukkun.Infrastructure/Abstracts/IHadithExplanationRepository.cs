using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IHadithExplanationRepository : IRepository<HadithExplanation>
    {
        Task<IEnumerable<HadithExplanation>> GetByHadithAsync(Guid hadithId);
    }
}
