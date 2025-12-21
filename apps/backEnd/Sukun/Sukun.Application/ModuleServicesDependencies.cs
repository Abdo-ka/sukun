using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sukun.Application.Implemantation;
using Sukun.Application.Interfaces;
using Sukun.Application.Seeder.AsumalHausna_entity;
using Sukun.Application.Seeder.Quran;
using Sukun.Application.Seeder.Tafsir_entity;

namespace Sukun.Application
{
    public static class ModuleApiServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IQuranService, QuranService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAsmaulHusnaService, AsmaulHusnaService>();
            services.AddScoped<INarrativeService, NarrativeService>();

            // Supporting services
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            // services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //Seeder
            services.AddScoped<IQuranSeederService, QuranSeederService>();
            services.AddScoped<ITafsirSeederService, TafsirSeederService>();
            services.AddScoped<IAsmaulHusnaSeederService, AsmaulHusnaSeederService>();
            return services;
        }
    }
}
