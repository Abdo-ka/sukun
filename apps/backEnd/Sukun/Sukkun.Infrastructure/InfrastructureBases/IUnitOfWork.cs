using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;

namespace Sukun.Infrastructure.InfrastructureBases
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        // Specific Repositories
        public IAdminRepository Admins { get; }
        public IAsmaulHusnaRepository AsmaulHusna { get; }
        public ICityRepository Cities { get; }
        public IQuranRepository Quran { get; }
        public INarrativeRepository Narrative { get; }
        public INarrativeSectionRepository NarrativeSection { get; }
        public IHadithCategoryRepository HadithCategories { get; }
        public IHadithRepository Hadiths { get; }
        public IHadithExplanationRepository HadithExplanations { get; }
        // Save changes
        public Task<int> CompleteAsync();
        public Task<bool> SaveEntitiesAsync();
        // Transaction support
        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
    }
}
