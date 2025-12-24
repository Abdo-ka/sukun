using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class MosqueRepository : Repository<Mosque>, IMosqueRepository
    {
        public MosqueRepository(ApplicationDbContext context, ILogger<Repository<Mosque>> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Mosque>> GetByCityAsync(Guid cityId)
        {
            return await _dbSet
                .Where(m => !m.IsDeleted && m.CityId == cityId)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Mosque>> GetNearbyAsync(double latitude, double longitude, double radiusInKm = 10)
        {
            // حساب المسافة باستخدام Haversine formula (تقريبي لكن فعال)
            var radiusInDegrees = radiusInKm / 111.0; // تقريب 1 درجة = 111 كم

            return await _dbSet
                .Where(m => !m.IsDeleted &&
                            m.Latitude > latitude - radiusInDegrees &&
                            m.Latitude < latitude + radiusInDegrees &&
                            m.Longitude > longitude - radiusInDegrees &&
                            m.Longitude < longitude + radiusInDegrees)
                .OrderBy(m => Math.Pow(m.Latitude - latitude, 2) + Math.Pow(m.Longitude - longitude, 2))
                .ToListAsync();
        }

        public async Task<IEnumerable<Mosque>> GetJummahMosquesAsync(Guid cityId)
        {
            return await _dbSet
                .Where(m => !m.IsDeleted && m.CityId == cityId && m.IsJummahMasjid)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }
    }
}
