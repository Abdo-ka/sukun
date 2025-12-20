namespace Sukun.Application.Dtos.QuranVerse.Response
{
    // للـ Quran

    public class QuranStatisticsDto
    {
        public int TotalSurahs { get; set; }
        public int TotalVerses { get; set; }
        public int MakkiSurahs { get; set; }
        public int MadaniSurahs { get; set; }
        public Dictionary<int, int> VersesByJuz { get; set; } = new();
        public Dictionary<int, int> VersesByPage { get; set; } = new();
    }
}

