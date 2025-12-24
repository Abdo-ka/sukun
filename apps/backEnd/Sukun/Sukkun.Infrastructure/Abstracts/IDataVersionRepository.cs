using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IDataVersionRepository : IRepository<DataVersion>
    {
        Task<Dictionary<string, long>> GetAllVersionsDictionaryAsync();
    }

}
