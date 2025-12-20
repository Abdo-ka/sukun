
using Microsoft.EntityFrameworkCore;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure;
using System.Text.Json.Serialization;
using System.Text.Json;
using Sukun.Application;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Sukun.Middleware;
using Microsoft.IdentityModel.Tokens;
using Sukun.Domin.Enums;
using Sukun.Application.Seeder.Quran;
using Sukun.Application.Seeder.Tafsir_entity;

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
            builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = services.GetRequiredService <ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
                var quranSeeder = services.GetRequiredService<IQuranSeederService>();
                var surahsCount = await dbContext.QuranSurahs.CountAsync();
                if (surahsCount == 0) // أو < 114
                {
                    await quranSeeder.SeedQuranAsync();
                }
                else
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogInformation("Quran already seeded, skipping...");
                }
                var tafsirsCount = await dbContext.Tafsirs.CountAsync(x=>x.Source == TafsirSource.Jalalayn);
                var tafsirSeeder = services.GetRequiredService<ITafsirSeederService>();
                if (tafsirsCount == 0) 
                {
                    await tafsirSeeder.SeedTafsirAsync(TafsirSource.Jalalayn); // أو IbnKathir, Saadi, etc.
                }
                else
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogInformation($"tafsir {TafsirSource.Jalalayn} already seeded, skipping...");
                }
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "Error! Database Not Updated");
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
