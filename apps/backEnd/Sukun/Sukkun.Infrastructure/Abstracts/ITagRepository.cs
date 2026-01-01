using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<Tag?> GetByNameAsync(string name);
    }
}
