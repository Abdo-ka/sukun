using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IAsmaulHusnaRepository : IRepository<AsmaulHusna>
    {
        Task<AsmaulHusna?> GetByNumberAsync(int number);
        Task<IEnumerable<AsmaulHusna>> GetRandomAsync(int count);
    }

}
