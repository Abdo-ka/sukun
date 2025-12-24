
using Microsoft.EntityFrameworkCore;
using Sukun.Application;
using Sukun.Application.Seeder;
using Sukun.Application.Seeder.AsumalHausna_entity;
using Sukun.Application.Seeder.Dua_entity;
using Sukun.Application.Seeder.Hadith_entity;
using Sukun.Application.Seeder.Quran;
using Sukun.Application.Seeder.Tafsir_entity;
using Sukun.Domin.Enums;
using Sukun.Infrastructure;
using Sukun.Infrastructure.Configuration;
using Sukun.Infrastructure.Context;
using Sukun.Middleware;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sukun.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            });
            ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                                                                                .AddInterceptors(new VersionInterceptor()));
            builder.Services.AddHttpClient();
            builder.Services.AddServicesDependencies(builder.Configuration)
                            .AddInfrastructureDependencies()
                            .AddApiServices(builder.Configuration);

            #region CORS
            var allowAll = "allowAll";
            builder.Services.AddCors(options =>
            {
                //  origin
                options.AddPolicy(allowAll,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            #endregion


            //Auth Filter
            var app = builder.Build();
            app.UseMiddleware<ErrorHandlerExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            #region Update-Database

            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Database migration completed successfully.");
                await DataSeeder.SeedAsync(dbContext);
                var quranSeeder = services.GetRequiredService<IQuranSeederService>();
                await quranSeeder.SeedQuranAsync();

                var tafsirSeeder = services.GetRequiredService<ITafsirSeederService>();
                await tafsirSeeder.SeedTafsirAsync(TafsirSource.Jalalayn); 

                var asmaulHusnaSeeder = services.GetRequiredService<IAsmaulHusnaSeederService>();
                await asmaulHusnaSeeder.SeedAsmaulHusnaAsync();

                var hadithSeeder = services.GetRequiredService<IHadithSeederService>();
                await hadithSeeder.SeedHadithsAsync();

                var duaSeeder = services.GetRequiredService<IDuaSeederService>();
                await duaSeeder.SeedDuasAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error! Database Not Updated");
            }

            #endregion

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors(allowAll);
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
