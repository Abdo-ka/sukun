using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.PrayerTime.Request
{
    public class DailyPrayerByCityRequest
    {
        public DateTime? Date { get; set; }
        public PrayerCalculationMethod Method { get; set; } = PrayerCalculationMethod.MuslimWorldLeague;
    }
}
