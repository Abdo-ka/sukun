using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sukun.Application.Implemantation;
using Sukun.Application.Interfaces;
using Sukun.Application.Seeder.AsumalHausna_entity;
using Sukun.Application.Seeder.Dua_entity;
using Sukun.Application.Seeder.Hadith_entity;
using Sukun.Application.Seeder.Quran;
using Sukun.Application.Seeder.Remembrance_entity;
using Sukun.Application.Seeder.Tafsir_entity;
using Sukun.Infrastructure.Abstracts;

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
            services.AddScoped<IHadithService, HadithService>();
            services.AddScoped<IHadithCategoryService, HadithCategoryService>();
            services.AddScoped<IHadithExplanationService, HadithExplanationService>();
            services.AddScoped<IIslamicBookService, IslamicBookService>();
            services.AddScoped<IIslamicBookSectionService, IslamicBookSectionService>();
            services.AddScoped<IBookContentService, BookContentService>();
            services.AddScoped<IDuaCategoryService, DuaCategoryService>();
            services.AddScoped<IDuaItemService, DuaItemService>();
            services.AddScoped<IRemembranceCategoryService, RemembranceCategoryService>();
            services.AddScoped<IRemembranceService, RemembranceService>();
            services.AddScoped<ITasbihService, TasbihService>();
            services.AddScoped<IMosqueService, MosqueService>();
            services.AddScoped<IPrayerTimeService, PrayerTimeService>();
            services.AddScoped<IDataVersionService, DataVersionService>();

            // Supporting services
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            // services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //Seeder
            services.AddScoped<IQuranSeederService, QuranSeederService>();
            services.AddScoped<ITafsirSeederService, TafsirSeederService>();
            services.AddScoped<IAsmaulHusnaSeederService, AsmaulHusnaSeederService>();
            services.AddScoped<IHadithSeederService, HadithSeederService>();
            services.AddScoped<IDuaSeederService, DuaSeederService>();
            services.AddScoped<IRemembranceSeederService, RemembranceSeederService>();
            return services;
        }
    }
}
