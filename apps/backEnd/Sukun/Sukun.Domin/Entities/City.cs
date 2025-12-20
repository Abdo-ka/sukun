namespace Sukun.Domin.Entities
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; } // Arabic name
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int TimeZone { get; set; }
        // Navigation Properties
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}




