namespace Sukun.Application.Dtos.Mosque.Request
{
    public class MosqueCreateDto
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public bool HasPrayerFacilities { get; set; } = true;
        public bool HasWomenSection { get; set; } = false;
        public bool HasParking { get; set; } = false;
        public bool IsJummahMasjid { get; set; } = false;
        public bool IsVerified { get; set; } = false;
    }
}
