using Sukun.Application.Dtos.QuranSurah.Response;
using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Application.Dtos.Tafsir.Response;
using Sukun.Domin.Common;
using Sukun.Domin.Enums;

namespace Sukun.Application.Interfaces
{
    public interface IQuranService
    {
        // Surah
        Task<Result<QuranSurahResponseDto>> GetSurahByNumberAsync(int surahNumber);
        Task<Result<QuranSurahResponseDto>> GetSurahByIdAsync(Guid surahId);
        Task<Result<QuranSurahWithVersesResponseDto>> GetSurahWithVersesAsync(int surahNumber);
        Task<Result<IEnumerable<QuranSurahResponseDto>>> GetAllSurahsAsync();
        Task<Result<IEnumerable<QuranSurahResponseDto>>> GetSurahsByRevelationTypeAsync(RevelationType revelationType);
        Task<Result<QuranSurahResponseDto>> GetSurahByRevelationOrderAsync(int order);

        // Verse
        Task<Result<QuranVerseResponseDto>> GetVerseByIdAsync(Guid verseId);
        Task<Result<QuranVerseWithDetailsResponseDto>> GetVerseWithDetailsAsync(Guid verseId);
        Task<Result<QuranVerseResponseDto>> GetVerseBySurahAndNumberAsync(int surahNumber, int verseNumber);
        Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesBySurahAsync(int surahNumber);
        Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesByPageAsync(int pageNumber);
        Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesByJuzAsync(int juzNumber);
        Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesByHizbAsync(int hizbNumber);
        Task<Result<QuranVerseResponseDto>> GetRandomVerseAsync();

        // Tafsir
        Task<Result<IEnumerable<TafsirResponseDto>>> GetTafsirsByVerseAsync(Guid verseId);
        Task<Result<TafsirResponseDto>> GetTafsirByVerseAndSourceAsync(Guid verseId, TafsirSource source);
        Task<Result<IEnumerable<TafsirResponseDto>>> GetTafsirsBySourceAsync(TafsirSource source);

        // Statistics
        Task<Result<int>> GetTotalVersesCountAsync();
        Task<Result<int>> GetTotalSurahCountAsync();
        Task<Result<int>> GetTafsirCountBySourceAsync(TafsirSource tafsirSource);
        Task<Result<int>> GetVersesCountBySurahAsync(int surahNumber);
        Task<Result<Dictionary<int, int>>> GetVersesCountByJuzAsync();

        //Task<Result<PagedResponseDto<QuranVerseResponseDto>>> SearchVersesAsync(SearchRequestDto request);
        Task<Result<Dictionary<int, int>>> GetVersesCountByPageAsync();
        Task<Result<QuranStatisticsDto>> GetQuranStatisticsAsync();
    }
}