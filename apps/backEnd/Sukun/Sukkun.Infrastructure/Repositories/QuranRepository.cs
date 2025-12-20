using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.Context;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Infrastructure.Repositories
{
    public class QuranRepository : IQuranRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<QuranRepository> _logger;
        private readonly IRepository<QuranSurah> _surahRepository;
        private readonly IRepository<QuranVerse> _verseRepository;
        private readonly IRepository<Tafsir> _tafsirRepository;

        public QuranRepository(
            ApplicationDbContext context,
            ILogger<QuranRepository> logger,
            IRepository<QuranSurah> surahRepository,
            IRepository<QuranVerse> verseRepository,
            IRepository<Tafsir> tafsirRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _surahRepository = surahRepository ?? throw new ArgumentNullException(nameof(surahRepository));
            _verseRepository = verseRepository ?? throw new ArgumentNullException(nameof(verseRepository));
            _tafsirRepository = tafsirRepository ?? throw new ArgumentNullException(nameof(tafsirRepository));
        }

        #region Surah Operations
        public async Task<QuranSurah?> GetSurahByNumberAsync(int surahNumber)
        {
            try
            {
                return await _surahRepository.FirstOrDefaultAsync(
                    s => s.Number == surahNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surah by number: {SurahNumber}", surahNumber);
                throw;
            }
        }

        public async Task<QuranSurah?> GetSurahWithVersesAsync(int surahNumber)
        {
            try
            {
                return await _surahRepository.AsQueryableNoTracking().Where(
                    s => s.Number == surahNumber

                    ).Include(v => v.Verses.OrderBy(v => v.VerseNumber)).FirstOrDefaultAsync()
                    ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surah with verses: {SurahNumber}", surahNumber);
                throw;
            }
        }

        public async Task<IEnumerable<QuranSurah>> GetAllSurahsAsync()
        {
            try
            {
                return await _surahRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all surahs");
                throw;
            }
        }

        public async Task<IEnumerable<QuranSurah>> GetSurahsByRevelationTypeAsync(RevelationType revelationType)
        {
            try
            {
                return await _surahRepository.FindAsync(
                    s => s.RevelationType == revelationType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surahs by revelation type: {RevelationType}", revelationType);
                throw;
            }
        }

        public async Task<QuranSurah?> GetSurahByRevelationOrderAsync(int order)
        {
            try
            {
                return await _surahRepository.FirstOrDefaultAsync(
                    s => s.RevelationOrder == order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surah by revelation order: {Order}", order);
                throw;
            }
        }
        #endregion

        #region Verse Operations
        public async Task<QuranVerse?> GetVerseByIdAsync(Guid verseId)
        {
            try
            {
                return await _verseRepository.GetByIdAsync(verseId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verse by ID: {VerseId}", verseId);
                throw;
            }
        }

        public async Task<QuranVerse?> GetVerseWithDetailsAsync(Guid verseId)
        {
            try
            {
                return await _verseRepository.GetByIdWithIncludesAsync(verseId,
                    v => v.Surah!,
                    v => v.Tafsirs!
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verse with details: {VerseId}", verseId);
                throw;
            }
        }

        public async Task<QuranVerse?> GetVerseBySurahAndNumberAsync(int surahNumber, int verseNumber)
        {
            try
            {
                return await _verseRepository.FirstOrDefaultAsync(
                    v => v.Surah!.Number == surahNumber && v.VerseNumber == verseNumber
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verse by surah and number: {SurahNumber}, {VerseNumber}", surahNumber, verseNumber);
                throw;
            }
        }

        public async Task<IEnumerable<QuranVerse>> GetVersesBySurahAsync(int surahNumber)
        {
            try
            {
                return await _verseRepository.FindAsync(
                    v => v.Surah!.Number == surahNumber
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by surah: {SurahNumber}", surahNumber);
                throw;
            }
        }

        public async Task<IEnumerable<QuranVerse>> GetVersesByPageAsync(int pageNumber)
        {
            try
            {
                return await _verseRepository.FindAsync(
                    v => v.PageNumber == pageNumber
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by page: {PageNumber}", pageNumber);
                throw;
            }
        }

        public async Task<IEnumerable<QuranVerse>> GetVersesByJuzAsync(int juzNumber)
        {
            try
            {
                return await _verseRepository.FindAsync(
                    v => v.JuzNumber == juzNumber
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by juz: {JuzNumber}", juzNumber);
                throw;
            }
        }

        public async Task<IEnumerable<QuranVerse>> GetVersesByHizbAsync(int hizbNumber)
        {
            try
            {
                return await _verseRepository.FindAsync(
                    v => v.HizbNumber == hizbNumber
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by hizb: {HizbNumber}", hizbNumber);
                throw;
            }
        }

        public async Task<IEnumerable<QuranVerse>> SearchVersesAsync(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                    return Enumerable.Empty<QuranVerse>();

                var normalizedSearchText = searchText.ToLower();

                // Note: For production, consider using Full-Text Search
                return await _verseRepository.FindAsync(
                    v => v.Text.ToLower().Contains(normalizedSearchText)
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching verses: {SearchText}", searchText);
                throw;
            }
        }

        public async Task<QuranVerse?> GetRandomVerseAsync()
        {
            try
            {
                var count = await _verseRepository.CountAsync();
                if (count == 0)
                    return null;

                var random = new Random();
                var skip = random.Next(0, count);

                return await _verseRepository.AsQueryable()
                    .Include(v => v.Surah)
                    .Skip(skip)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting random verse");
                throw;
            }
        }
        #endregion

        #region Tafsir Operations
        public async Task<IEnumerable<Tafsir>> GetTafsirsByVerseAsync(Guid verseId)
        {
            try
            {
                return await _tafsirRepository.FindAsync(
                    t => t.VerseId == verseId
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tafsirs by verse: {VerseId}", verseId);
                throw;
            }
        }

        public async Task<Tafsir?> GetTafsirByVerseAndSourceAsync(Guid verseId, TafsirSource source)
        {
            try
            {
                return await _tafsirRepository.FirstOrDefaultAsync(
                    t => t.VerseId == verseId && t.Source == source
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tafsir by verse and source: {VerseId}, {Source}", verseId, source);
                throw;
            }
        }

        public async Task<IEnumerable<Tafsir>> GetTafsirsBySourceAsync(TafsirSource source)
        {
            try
            {
                return await _tafsirRepository.FindAsync(
                    t => t.Source == source
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tafsirs by source: {Source}", source);
                throw;
            }
        }
        #endregion

        #region Statistics
        public async Task<int> GetTotalVersesCountAsync()
        {
            try
            {
                return await _verseRepository.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total verses count");
                throw;
            }
        }
        public async Task<int> GetTotalSurahCountAsync()
        {
            try
            {
                return await _surahRepository.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total verses count");
                throw;
            }
        }
        public async Task<int> GetTafsirCountBySourceAsync(TafsirSource source)
        {
            try
            {
                return await _tafsirRepository.CountAsync(x=>x.Source ==source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting total Tafsir {source} count");
                throw;
            }
        }

        public async Task<int> GetVersesCountBySurahAsync(int surahNumber)
        {
            try
            {
                return await _verseRepository.CountAsync(
                    v => v.Surah!.Number == surahNumber
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses count by surah: {SurahNumber}", surahNumber);
                throw;
            }
        }

        public async Task<Dictionary<int, int>> GetVersesCountByJuzAsync()
        {
            try
            {
                var result = await _verseRepository.AsQueryable()
                    .GroupBy(v => v.JuzNumber)
                    .Select(g => new { JuzNumber = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(x => x.JuzNumber, x => x.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses count by juz");
                throw;
            }
        }
        #endregion
    }
}
