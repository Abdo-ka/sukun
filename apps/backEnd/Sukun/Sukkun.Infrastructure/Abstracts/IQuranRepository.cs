using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Infrastructure.Abstracts
{
    public interface IQuranRepository
    {
        // Surah operations
        Task<QuranSurah?> GetSurahByNumberAsync(int surahNumber);
        Task<QuranSurah?> GetSurahWithVersesAsync(int surahNumber);
        Task<IEnumerable<QuranSurah>> GetAllSurahsAsync();
        Task<IEnumerable<QuranSurah>> GetSurahsByRevelationTypeAsync(RevelationType revelationType);
        Task<QuranSurah?> GetSurahByRevelationOrderAsync(int order);

        // Verse operations
        Task<QuranVerse?> GetVerseByIdAsync(Guid verseId);
        Task<QuranVerse?> GetVerseWithDetailsAsync(Guid verseId);
        Task<QuranVerse?> GetVerseBySurahAndNumberAsync(int surahNumber, int verseNumber);
        Task<IEnumerable<QuranVerse>> GetVersesBySurahAsync(int surahNumber);
        Task<IEnumerable<QuranVerse>> GetVersesByPageAsync(int pageNumber);
        Task<IEnumerable<QuranVerse>> GetVersesByJuzAsync(int juzNumber);
        Task<IEnumerable<QuranVerse>> GetVersesByHizbAsync(int hizbNumber);
        Task<IEnumerable<QuranVerse>> SearchVersesAsync(string searchText);
        Task<QuranVerse?> GetRandomVerseAsync();

        // Tafsir operations
        Task<IEnumerable<Tafsir>> GetTafsirsByVerseAsync(Guid verseId);
        Task<Tafsir?> GetTafsirByVerseAndSourceAsync(Guid verseId, TafsirSource source);
        Task<IEnumerable<Tafsir>> GetTafsirsBySourceAsync(TafsirSource source);
        Task<int> GetTafsirCountBySourceAsync(TafsirSource source);

        // Statistics
        Task<int> GetTotalVersesCountAsync();
        Task<int> GetTotalSurahCountAsync();
        Task<int> GetVersesCountBySurahAsync(int surahNumber);
        Task<Dictionary<int, int>> GetVersesCountByJuzAsync();
    }
}
