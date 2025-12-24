namespace Sukun.Application.Dtos.PrayerTime.Response
{
    public class PrayerTimeResponseDto
    {
        public string CityName { get; set; } = string.Empty;
        public List<DailyPrayerTimeDto> DailyTimes { get; set; } = new(); // لسنة كاملة أو يوم واحد
        public string? NextPrayer { get; set; }
        public string? TimeUntilNextPrayer { get; set; }
    }

}
