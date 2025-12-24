using Sukun.Domin.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukun.Application.Dtos.PrayerTime.Response
{
    public class DailyPrayerTimeDto
    {
        public string GregorianDate { get; set; } = string.Empty;
        public string HijriDate { get; set; } = string.Empty;
        public string Fajr { get; set; } = string.Empty;
        public string Sunrise { get; set; } = string.Empty;
        public string Dhuhr { get; set; } = string.Empty;
        public string Asr { get; set; } = string.Empty;
        public string Maghrib { get; set; } = string.Empty;
        public string Isha { get; set; } = string.Empty;
    }
}
