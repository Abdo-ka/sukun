using Sukun.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.PrayerTime.Request
{
    public class DailyPrayerByLocationRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? Date { get; set; }
        public PrayerCalculationMethod Method { get; set; } = PrayerCalculationMethod.MuslimWorldLeague;
    }
}
