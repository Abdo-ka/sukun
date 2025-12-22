using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IIslamicBookRepository : IRepository<IslamicBook>
    {
        Task<IEnumerable<IslamicBook>> GetAllWithDetailsAsync();
        Task<IslamicBook?> GetByIdWithSectionsAsync(Guid id);
        Task<IEnumerable<IslamicBook>> GetByTypeAsync(BookType type);
    }
}
