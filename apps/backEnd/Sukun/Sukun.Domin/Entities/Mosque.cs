namespace Sukun.Domin.Entities
{
    public class Mosque : BaseEntity
    {
        public Guid CityId { get; set; }

        // Mosque Info
        public string Name { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;          // للدعم العربي
        public string Address { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Contact
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }

        // Features
        public bool HasPrayerFacilities { get; set; } = true;
        public bool HasWomenSection { get; set; } = false;
        public bool HasParking { get; set; } = false;
        public bool IsJummahMasjid { get; set; } = false;

        // Status
        public bool IsVerified { get; set; } = false;
        public int Rating { get; set; } = 0;                        // متوسط التقييم (0-5)
        public int ReviewCount { get; set; } = 0;

        // Navigation
        public virtual City City { get; set; } = null!;
    }

}




