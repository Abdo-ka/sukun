using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface INarrativeRepository : IRepository<Narrative>
    {
        Task<IEnumerable<Narrative>> GetAllWithDetailsAsync();
        Task<Narrative?> GetByIdWithFullDetailsAsync(Guid id); 
        Task<IEnumerable<Narrative>> GetRootNarrativesAsync();
        Task<IEnumerable<Narrative>> GetFeaturedAsync();
        Task<IEnumerable<Narrative>> GetByTypeAsync(ContentType type);
        Task<IEnumerable<Narrative>> GetChildrenAsync(Guid parentId);
        Task<Narrative?> GetByTagAsync(NarrativeTag tag);
        Task<IEnumerable<Narrative>> GetChildrenOfTaggedSectionAsync(NarrativeTag tag);

    }
}
