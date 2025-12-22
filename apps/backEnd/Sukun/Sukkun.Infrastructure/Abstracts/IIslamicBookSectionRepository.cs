using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IIslamicBookSectionRepository : IRepository<IslamicBookSection>
    {
        Task<IEnumerable<IslamicBookSection>> GetByBookAsync(Guid bookId);
    }
}
