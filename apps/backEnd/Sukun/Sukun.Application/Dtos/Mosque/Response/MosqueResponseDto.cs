namespace Sukun.Application.Dtos.Mosque.Response
{
    public class MosqueResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public bool HasPrayerFacilities { get; set; }
        public bool HasWomenSection { get; set; }
        public bool HasParking { get; set; }
        public bool IsJummahMasjid { get; set; }
        public bool IsVerified { get; set; }
        public int Rating { get; set; }
        public int ReviewCount { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}
