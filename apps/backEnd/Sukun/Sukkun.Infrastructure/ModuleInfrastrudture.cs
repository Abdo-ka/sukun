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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IQuranRepository, QuranRepository>();
            services.AddScoped<IQuranRepository, QuranRepository>();
            services.AddScoped<IUserDeviceRepository, UserDeviceRepository>();
            services.AddScoped<IFCMTokenRepository, FCMTokenRepository>();
            services.AddScoped<IUserBookmarkRepository, UserBookmarkRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            return services;
        }
            
    }
}
