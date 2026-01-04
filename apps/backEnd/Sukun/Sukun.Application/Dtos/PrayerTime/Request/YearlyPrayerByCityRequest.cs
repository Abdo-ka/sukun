using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.PrayerTime.Request
{
    public class YearlyPrayerByCityRequest
    {
        public int Year { get; set; }
        public PrayerCalculationMethod Method { get; set; } = PrayerCalculationMethod.MuslimWorldLeague;
    }
}
