using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface INarrativeRepository : IRepository<Narrative>
    {
        Task<IEnumerable<Narrative>> GetAllWithDetailsAsync(ContentType? type = null, Guid? tagId = null, Guid? categoryId = null);

        Task<Narrative?> GetByTagAndTypeAsync(Guid tag, ContentType? type = null);
        Task<IEnumerable<Narrative>> GetFeaturedAsync(int count = 10);
        Task<IEnumerable<Narrative>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Narrative>> GetByTagAsync(Guid tagId);
        Task<Narrative?> GetByIdWithDetailsAsync(Guid id, ContentType? type = null, Guid? tagId = null, Guid? categoryId = null);
        Task<IEnumerable<Narrative>> GetByTypeAsync(ContentType type);

    }
}
