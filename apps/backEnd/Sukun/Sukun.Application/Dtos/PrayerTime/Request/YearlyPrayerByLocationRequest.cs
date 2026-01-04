using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.PrayerTime.Request
{
    public class YearlyPrayerByLocationRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Year { get; set; }
        public PrayerCalculationMethod Method { get; set; } = PrayerCalculationMethod.MuslimWorldLeague;
    }
}
