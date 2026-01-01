using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Repositories;

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
        public IBookContentRepository BookContent { get; }
        public IIslamicBookRepository IslamicBook { get; }
        public IIslamicBookSectionRepository IslamicBookSection { get; }
        public IDuaCategoryRepository DuaCategoryRepository { get; }
        public IDuaItemRepository DuaItemRepository { get; }
        public IRemembranceCategoryRepository RemembranceCategories { get; }
        public IRemembranceRepository Remembrances { get; }
        public IRemembranceContentRepository RemembranceContents { get; }
        public ITasbihRepository Tasbihs { get; }
        public IMosqueRepository Mosques { get; }
        public ICategoryRepository Categories { get; }
        public INarrativeCategoryRepository NarrativeCategories { get; }
        public INarrativeTagsRepository NarrativeTags { get; }
        public ITagRepository Tags { get; }
        // Save changes
        public Task<int> CompleteAsync();
        public Task<bool> SaveEntitiesAsync();
        // Transaction support
        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
    }
}
