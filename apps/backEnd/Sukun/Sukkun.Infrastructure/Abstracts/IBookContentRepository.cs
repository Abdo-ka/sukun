using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IBookContentRepository : IRepository<BookContent>
    {
        Task<IEnumerable<BookContent>> GetBySectionAsync(Guid sectionId);
    }
}
