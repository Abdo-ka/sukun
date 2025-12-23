using Microsoft.Extensions.DependencyInjection;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;
using Sukun.Infrastructure.Repositories;
namespace Sukun.Infrastructure
{
    public static class ModuleInfrastrudtureDepndencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Generic Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
           
            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
     
            // Specific Repositories
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IQuranRepository, QuranRepository>();
            services.AddScoped<IQuranRepository, QuranRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAsmaulHusnaRepository, AsmaulHusnaRepository>();
            services.AddScoped<INarrativeRepository, NarrativeRepository>();
            services.AddScoped<INarrativeSectionRepository, NarrativeSectionRepository>();
            services.AddScoped<IHadithCategoryRepository, HadithCategoryRepository>();
            services.AddScoped<IHadithRepository, HadithRepository>();
            services.AddScoped<IHadithExplanationRepository, HadithExplanationRepository>();
            services.AddScoped<IIslamicBookRepository, IslamicBookRepository>();
            services.AddScoped<IIslamicBookSectionRepository, IslamicBookSectionRepository>();
            services.AddScoped<IBookContentRepository, BookContentRepository>();
            services.AddScoped<IDuaCategoryRepository, DuaCategoryRepository>();
            services.AddScoped<IDuaItemRepository, DuaItemRepository>();
            return services;
        }
            
    }
}
