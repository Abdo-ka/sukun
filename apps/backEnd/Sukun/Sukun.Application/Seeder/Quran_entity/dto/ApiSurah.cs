namespace Sukun.Application.Seeder.Quran.dto
{
    public class ApiSurah
    {
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
        public string EnglishNameTranslation { get; set; } = string.Empty;
        public string RevelationType { get; set; } = string.Empty;
        public int RevelationOrder { get; set; }
        public List<ApiAyah> Ayahs { get; set; } = new();
    }
}
