namespace Sukun.Application.Dtos.City.Request
{
    public class CityCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int TimeZone { get; set; }
    }
}

