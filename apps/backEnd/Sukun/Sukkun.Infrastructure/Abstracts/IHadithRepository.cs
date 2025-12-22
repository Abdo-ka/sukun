using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IHadithRepository : IRepository<Hadith>
    {
        Task<IEnumerable<Hadith>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Hadith>> GetRandomAsync(int count = 1);
        Task<Hadith?> GetByIdWithDetailsAsync(Guid id); // مع Explanations و Category
        Task<IEnumerable<Hadith>> SearchAsync(string query);
    }
}
