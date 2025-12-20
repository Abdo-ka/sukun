using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.QuranSurah.Response
{
    public class QuranSurahWithVersesResponseDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public int RevelationOrder { get; set; }
        public RevelationType RevelationType { get; set; }
        public int TotalVerses { get; set; }
        public IEnumerable<QuranVerseResponseDto> Verses { get; set; } = new List<QuranVerseResponseDto>();
    }
}

