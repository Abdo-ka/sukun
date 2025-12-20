namespace Sukun.Application.Dtos.QuranSurah.Response
{
    public class QuranSurahSimpleDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
    }
}

