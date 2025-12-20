using Sukun.Application.Dtos.QuranSurah.Response;
using Sukun.Application.Dtos.Tafsir.Response;

namespace Sukun.Application.Dtos.QuranVerse.Response
{
    public class QuranVerseWithDetailsResponseDto
    {
        public Guid Id { get; set; }
        public Guid SurahId { get; set; }
        public int VerseNumber { get; set; }
        public int PageNumber { get; set; }
        public int JuzNumber { get; set; }
        public int HizbNumber { get; set; }
        public string Text { get; set; }
        public QuranSurahResponseDto Surah { get; set; }
        public IEnumerable<TafsirResponseDto> Tafsirs { get; set; } = new List<TafsirResponseDto>();
    }
}

