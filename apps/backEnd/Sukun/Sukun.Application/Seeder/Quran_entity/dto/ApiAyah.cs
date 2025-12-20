namespace Sukun.Application.Seeder.Quran.dto
{

    public class ApiAyah
    {
        public int Number { get; set; }
        public int NumberInSurah { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Page { get; set; }
        public int Juz { get; set; }
        public int HizbQuarter { get; set; }
    }
}
