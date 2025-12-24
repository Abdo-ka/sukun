using Sukun.Domin.Entities;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IMosqueRepository : IRepository<Mosque>
    {
        Task<IEnumerable<Mosque>> GetByCityAsync(Guid cityId);
        Task<IEnumerable<Mosque>> GetNearbyAsync(double latitude, double longitude, double radiusInKm = 10);
        Task<IEnumerable<Mosque>> GetJummahMosquesAsync(Guid cityId);
    }

}
