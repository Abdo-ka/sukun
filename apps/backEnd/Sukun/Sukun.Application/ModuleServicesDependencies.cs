using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Sukun.Application
{
    public static class ModuleApiServicesDependencies
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
        {      
            return services;
        }
    }
}
