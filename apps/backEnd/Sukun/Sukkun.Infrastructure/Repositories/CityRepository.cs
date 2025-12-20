using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context, ILogger<CityRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<City?> GetByNameAndCountryAsync(string name, string countryCode)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(c => c.Name == name && c.CountryCode == countryCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting city by name and country: {Name}, {CountryCode}", name, countryCode);
                throw;
            }
        }

        public async Task<IEnumerable<City>> GetCitiesByCountryAsync(string countryCode)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.CountryCode == countryCode)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities by country: {CountryCode}", countryCode);
                throw;
            }
        }

        public async Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllAsync();

                var normalizedSearchTerm = searchTerm.ToLower();

                return await _dbSet
                    .Where(c => c.Name.ToLower().Contains(normalizedSearchTerm) ||
                                c.NameAr.ToLower().Contains(normalizedSearchTerm) ||
                                c.Country.ToLower().Contains(normalizedSearchTerm))
                    .OrderBy(c => c.Name)
                    .Take(100) // Limit results
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching cities with term: {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<IEnumerable<City>> GetCitiesByTimeZoneAsync(int timeZone)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.TimeZone == timeZone)
                    .OrderBy(c => c.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities by timezone: {TimeZone}", timeZone);
                throw;
            }
        }

        public async Task<City?> FindNearestCityAsync(double latitude, double longitude)
        {
            try
            {
                var cities = await GetAllAsync();

                return cities
                    .OrderBy(c => CalculateDistance(latitude, longitude, c.Latitude, c.Longitude))
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding nearest city for coordinates: {Latitude}, {Longitude}", latitude, longitude);
                throw;
            }
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Haversine formula
            var earthRadius = 6371; // km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return earthRadius * c;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public async Task<int> CountCitiesByCountryAsync(string countryCode)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.CountryCode == countryCode)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting cities by country: {CountryCode}", countryCode);
                throw;
            }
        }

        public async Task<Result<City>> UpdateCoordinatesAsync(Guid cityId, double latitude, double longitude)
        {
            try
            {
                var city = await GetByIdAsync(cityId);
                if (city == null)
                    return Result<City>.Failure($"City with ID {cityId} not found");

                city.Latitude = latitude;
                city.Longitude = longitude;
                city.UpdatedAt = DateTime.UtcNow;

                _dbSet.Update(city);
                await _context.SaveChangesAsync();

                return Result<City>.Success(city);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating coordinates for city: {CityId}", cityId);
                return Result<City>.Failure(ex.Message);
            }
        }
    }
}
