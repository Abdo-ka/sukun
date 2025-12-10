using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City?> GetByNameAndCountryAsync(string name, string countryCode);
        Task<IEnumerable<City>> GetCitiesByCountryAsync(string countryCode);
        Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm);
        Task<IEnumerable<City>> GetCitiesWithUsersAsync();
        Task<City?> GetCityWithUsersAsync(Guid cityId);
        Task<IEnumerable<City>> GetCitiesByTimeZoneAsync(int timeZone);
        Task<City?> FindNearestCityAsync(double latitude, double longitude);
        Task<int> CountCitiesByCountryAsync(string countryCode);
        Task<Result<City>> UpdateCoordinatesAsync(Guid cityId, double latitude, double longitude);
    }
}
