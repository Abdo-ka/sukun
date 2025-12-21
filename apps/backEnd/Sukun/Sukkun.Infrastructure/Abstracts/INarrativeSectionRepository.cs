using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface INarrativeSectionRepository : IRepository<NarrativeSection>
    {
        Task<NarrativeSection?> GetByIdWithNarrativeAsync(Guid sectionId);
    }
}
