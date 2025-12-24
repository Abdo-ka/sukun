namespace Sukun.Application.Dtos.PrayerTime.Response
{
    public class AladhanDate
    {
        public string Readable { get; set; } = string.Empty;
        public AladhanHijri Hijri { get; set; } = null!;
    }
}
