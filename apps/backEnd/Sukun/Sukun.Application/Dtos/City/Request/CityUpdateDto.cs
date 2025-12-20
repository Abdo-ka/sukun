namespace Sukun.Application.Dtos.City.Request
{
    public class CityUpdateDto
    {
        public string? Name { get; set; }
        public string? NameAr { get; set; }
        public string? Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? TimeZone { get; set; }
    }
}

