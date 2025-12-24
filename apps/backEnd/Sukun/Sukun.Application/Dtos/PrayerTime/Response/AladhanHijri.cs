namespace Sukun.Application.Dtos.PrayerTime.Response
{
    public class AladhanHijri
    {
        public int Day { get; set; }
        public string Date { get; set; } = string.Empty;
        public AladhanHijriMonth Month { get; set; } = null!;
        public int Year { get; set; }
    }
}
