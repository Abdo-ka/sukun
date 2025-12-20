using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sukun.Api.Filter;
using Sukun.Application.Dtos.Admin.Request;
using Sukun.Application.Dtos.Admin.Validators;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // التحكم في JSON
        services.AddControllers(op => op.Filters.Add(typeof(ValidationFilter)))
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<AdminUpdateDtoValidator>();



        services.AddSwaggerConfiguration();
        services.AddJwtAuthentication(configuration);

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sukun", Version = "v1" });
            // c.EnableAnnotations();

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
        });

        return services;
    }
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = Boolean.Parse(jwtSettings["validateIssuer"]!),
                    ValidIssuer = jwtSettings["issuer"],
                    ValidateAudience = Boolean.Parse(jwtSettings["validateAudience"]),
                    ValidAudience = jwtSettings["audience"],
                    ValidateIssuerSigningKey = Boolean.Parse(jwtSettings["validateIssuerSigningKey"]!),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secret"]!)),
                    ValidateLifetime = Boolean.Parse(jwtSettings["validateLifetime"]!),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        return services;
    }
}