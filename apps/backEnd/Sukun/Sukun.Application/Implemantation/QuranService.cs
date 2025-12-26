using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.QuranSurah.Response;
using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Application.Dtos.Tafsir.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{

    public class QuranService : BaseService, IQuranService
    {


        public QuranService(IUnitOfWork unitOfWork, ILogger<QuranService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Result<QuranSurahResponseDto>> GetSurahByNumberAsync(int surahNumber)
        {
            try
            {
                _logger.LogDebug("Getting surah by number: {SurahNumber}", surahNumber);
                var surah = await _unitOfWork.Quran.GetSurahByNumberAsync(surahNumber);
                if (surah == null)
                    return Result<QuranSurahResponseDto>.NotFound($"Surah {surahNumber} not found");

                return Result<QuranSurahResponseDto>.Success(QuranMapper.ToResponseDto(surah));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surah by number: {SurahNumber}", surahNumber);
                return Result<QuranSurahResponseDto>.Failure($"Error retrieving surah: {ex.Message}");
            }
        }

        public async Task<Result<QuranSurahResponseDto>> GetSurahByIdAsync(Guid surahId)
        {
            try
            {
                _logger.LogDebug("Getting surah by ID: {SurahId}", surahId);
                var surah = await _unitOfWork.Repository<QuranSurah>().GetByIdAsync(surahId);
                if (surah == null)
                    return Result<QuranSurahResponseDto>.NotFound($"Surah with ID {surahId} not found");

                return Result<QuranSurahResponseDto>.Success(QuranMapper.ToResponseDto(surah));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surah by ID: {SurahId}", surahId);
                return Result<QuranSurahResponseDto>.Failure($"Error retrieving surah: {ex.Message}");
            }
        }

        public async Task<Result<QuranSurahWithVersesResponseDto>> GetSurahWithVersesAsync(int surahNumber)
        {
            try
            {
                _logger.LogDebug("Getting surah with verses: {SurahNumber}", surahNumber);
                var surah = await _unitOfWork.Quran.GetSurahWithVersesAsync(surahNumber);
                if (surah == null)
                    return Result<QuranSurahWithVersesResponseDto>.NotFound($"Surah {surahNumber} not found");

                return Result<QuranSurahWithVersesResponseDto>.Success(QuranMapper.ToDetailDto(surah));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surah with verses: {SurahNumber}", surahNumber);
                return Result<QuranSurahWithVersesResponseDto>.Failure($"Error retrieving surah: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<QuranSurahResponseDto>>> GetAllSurahsAsync()
        {
            try
            {
                _logger.LogDebug("Getting all surahs");
                var surahs = await _unitOfWork.Quran.GetAllSurahsAsync();
                return Result<IEnumerable<QuranSurahResponseDto>>.Success(surahs.OrderBy(x=>x.Number).Select(x => QuranMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all surahs");
                return Result<IEnumerable<QuranSurahResponseDto>>.Failure($"Error retrieving surahs: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<QuranSurahResponseDto>>> GetSurahsByRevelationTypeAsync(RevelationType revelationType)
        {
            try
            {
                _logger.LogDebug("Getting surahs by revelation type: {RevelationType}", revelationType);
                var surahs = await _unitOfWork.Quran.GetSurahsByRevelationTypeAsync(revelationType);
                return Result<IEnumerable<QuranSurahResponseDto>>.Success(surahs.OrderBy(x => x.Number).Select(x => QuranMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surahs by revelation type: {RevelationType}", revelationType);
                return Result<IEnumerable<QuranSurahResponseDto>>.Failure($"Error retrieving surahs: {ex.Message}");
            }
        }

        public async Task<Result<QuranSurahResponseDto>> GetSurahByRevelationOrderAsync(int order)
        {
            try
            {
                _logger.LogDebug("Getting surah by revelation order: {Order}", order);
                var surah = await _unitOfWork.Quran.GetSurahByRevelationOrderAsync(order);
                if (surah == null)
                    return Result<QuranSurahResponseDto>.NotFound($"Surah with revelation order {order} not found");

                return Result<QuranSurahResponseDto>.Success(QuranMapper.ToResponseDto(surah));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting surah by revelation order: {Order}", order);
                return Result<QuranSurahResponseDto>.Failure($"Error retrieving surah: {ex.Message}");
            }
        }

        public async Task<Result<QuranVerseResponseDto>> GetVerseByIdAsync(Guid verseId)
        {
            try
            {
                _logger.LogDebug("Getting verse by ID: {VerseId}", verseId);
                var verse = await _unitOfWork.Quran.GetVerseByIdAsync(verseId);
                if (verse == null)
                    return Result<QuranVerseResponseDto>.NotFound($"Verse with ID {verseId} not found");

                return Result<QuranVerseResponseDto>.Success(QuranMapper.ToResponseDto(verse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verse by ID: {VerseId}", verseId);
                return Result<QuranVerseResponseDto>.Failure($"Error retrieving verse: {ex.Message}");
            }
        }

        public async Task<Result<QuranVerseWithDetailsResponseDto>> GetVerseWithDetailsAsync(Guid verseId)
        {
            try
            {
                _logger.LogDebug("Getting verse with details by ID: {VerseId}", verseId);
                var verse = await _unitOfWork.Quran.GetVerseWithDetailsAsync(verseId);
                if (verse == null)
                    return Result<QuranVerseWithDetailsResponseDto>.NotFound($"Verse with ID {verseId} not found");

                return Result<QuranVerseWithDetailsResponseDto>.Success(QuranMapper.ToDetailDto(verse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verse with details: {VerseId}", verseId);
                return Result<QuranVerseWithDetailsResponseDto>.Failure($"Error retrieving verse: {ex.Message}");
            }
        }

        public async Task<Result<QuranVerseResponseDto>> GetVerseBySurahAndNumberAsync(int surahNumber, int verseNumber)
        {
            try
            {
                _logger.LogDebug("Getting verse by surah {SurahNumber} and verse {VerseNumber}", surahNumber, verseNumber);
                var verse = await _unitOfWork.Quran.GetVerseBySurahAndNumberAsync(surahNumber, verseNumber);
                if (verse == null)
                    return Result<QuranVerseResponseDto>.NotFound($"Verse {verseNumber} in surah {surahNumber} not found");

                return Result<QuranVerseResponseDto>.Success(QuranMapper.ToResponseDto(verse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verse by surah and number");
                return Result<QuranVerseResponseDto>.Failure($"Error retrieving verse: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesBySurahAsync(int surahNumber)
        {
            try
            {
                _logger.LogDebug("Getting verses by surah: {SurahNumber}", surahNumber);
                var verses = await _unitOfWork.Quran.GetVersesBySurahAsync(surahNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Success(verses.OrderBy(x => x.VerseNumber).Select(x => QuranMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by surah: {SurahNumber}", surahNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Failure($"Error retrieving verses: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesByPageAsync(int pageNumber)
        {
            try
            {
                _logger.LogDebug("Getting verses by page: {PageNumber}", pageNumber);
                var verses = await _unitOfWork.Quran.GetVersesByPageAsync(pageNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Success(verses.OrderBy(x => x.VerseNumber).Select(x => QuranMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by page: {PageNumber}", pageNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Failure($"Error retrieving verses: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesByJuzAsync(int juzNumber)
        {
            try
            {
                _logger.LogDebug("Getting verses by juz: {JuzNumber}", juzNumber);
                var verses = await _unitOfWork.Quran.GetVersesByJuzAsync(juzNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Success(verses.OrderBy(x => x.VerseNumber).Select(x => QuranMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by juz: {JuzNumber}", juzNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Failure($"Error retrieving verses: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<QuranVerseResponseDto>>> GetVersesByHizbAsync(int hizbNumber)
        {
            try
            {
                _logger.LogDebug("Getting verses by hizb: {HizbNumber}", hizbNumber);
                var verses = await _unitOfWork.Quran.GetVersesByHizbAsync(hizbNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Success(verses.OrderBy(x => x.VerseNumber).Select(x => QuranMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses by hizb: {HizbNumber}", hizbNumber);
                return Result<IEnumerable<QuranVerseResponseDto>>.Failure($"Error retrieving verses: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<QuranVerseResponseDto>>> SearchVersesAsync(string searchText)
        {
            try
            {
                _logger.LogDebug("Searching verses with text: {SearchText}", searchText);
                var verses = await _unitOfWork.Quran.SearchVersesAsync(searchText);
                return Result<IEnumerable<QuranVerseResponseDto>>.Success(verses.OrderBy(x => x.VerseNumber).Select(x => QuranMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching verses: {SearchText}", searchText);
                return Result<IEnumerable<QuranVerseResponseDto>>.Failure($"Error searching verses: {ex.Message}");
            }
        }

        public async Task<Result<QuranVerseResponseDto>> GetRandomVerseAsync()
        {
            try
            {
                _logger.LogDebug("Getting random verse");
                var verse = await _unitOfWork.Quran.GetRandomVerseAsync();
                if (verse == null)
                    return Result<QuranVerseResponseDto>.NotFound("No verses found");

                return Result<QuranVerseResponseDto>.Success(QuranMapper.ToResponseDto(verse));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting random verse");
                return Result<QuranVerseResponseDto>.Failure($"Error retrieving random verse: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<TafsirResponseDto>>> GetTafsirsByVerseAsync(Guid verseId)
        {
            try
            {
                _logger.LogDebug("Getting tafsirs by verse: {VerseId}", verseId);
                var tafsirs = await _unitOfWork.Quran.GetTafsirsByVerseAsync(verseId);
                return Result<IEnumerable<TafsirResponseDto>>.Success(tafsirs.Select(x => QuranMapper.ToTafsirResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tafsirs by verse: {VerseId}", verseId);
                return Result<IEnumerable<TafsirResponseDto>>.Failure($"Error retrieving tafsirs: {ex.Message}");
            }
        }

        public async Task<Result<TafsirResponseDto>> GetTafsirByVerseAndSourceAsync(Guid verseId, TafsirSource source)
        {
            try
            {
                _logger.LogDebug("Getting tafsir by verse and source: {VerseId}, {Source}", verseId, source);
                var tafsir = await _unitOfWork.Quran.GetTafsirByVerseAndSourceAsync(verseId, source);
                if (tafsir == null)
                    return Result<TafsirResponseDto>.NotFound($"Tafsir not found for verse {verseId} and source {source}");

                return Result<TafsirResponseDto>.Success(QuranMapper.ToTafsirResponseDto(tafsir));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tafsir by verse and source");
                return Result<TafsirResponseDto>.Failure($"Error retrieving tafsir: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<TafsirResponseDto>>> GetTafsirsBySourceAsync(TafsirSource source)
        {
            try
            {
                _logger.LogDebug("Getting tafsirs by source: {Source}", source);
                var tafsirs = await _unitOfWork.Quran.GetTafsirsBySourceAsync(source);
                return Result<IEnumerable<TafsirResponseDto>>.Success(tafsirs.Select(x => QuranMapper.ToTafsirResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tafsirs by source: {Source}", source);
                return Result<IEnumerable<TafsirResponseDto>>.Failure($"Error retrieving tafsirs: {ex.Message}");
            }
        }

        public async Task<Result<int>> GetTotalVersesCountAsync()
        {
            try
            {
                _logger.LogDebug("Getting total verses count");
                var count = await _unitOfWork.Quran.GetTotalVersesCountAsync();
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total verses count");
                return Result<int>.Failure($"Error retrieving count: {ex.Message}");
            }
        }
        public async Task<Result<int>> GetTotalSurahCountAsync()
        {
            try
            {
                _logger.LogDebug("Getting total verses count");
                var count = await _unitOfWork.Quran.GetTotalSurahCountAsync();
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total verses count");
                return Result<int>.Failure($"Error retrieving count: {ex.Message}");
            }
        }
        public async Task<Result<int>> GetTafsirCountBySourceAsync(TafsirSource tafsirSource)
        {
            try
            {
                _logger.LogDebug("Getting total verses count");
                var count = await _unitOfWork.Quran.GetTafsirCountBySourceAsync(tafsirSource);
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total verses count");
                return Result<int>.Failure($"Error retrieving count: {ex.Message}");
            }
        }

        public async Task<Result<int>> GetVersesCountBySurahAsync(int surahNumber)
        {
            try
            {
                _logger.LogDebug("Getting verses count by surah: {SurahNumber}", surahNumber);
                var count = await _unitOfWork.Quran.GetVersesCountBySurahAsync(surahNumber);
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses count by surah: {SurahNumber}", surahNumber);
                return Result<int>.Failure($"Error retrieving count: {ex.Message}");
            }
        }

        public async Task<Result<Dictionary<int, int>>> GetVersesCountByJuzAsync()
        {
            try
            {
                _logger.LogDebug("Getting verses count by juz");
                var counts = await _unitOfWork.Quran.GetVersesCountByJuzAsync();
                return Result<Dictionary<int, int>>.Success(counts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses count by juz");
                return Result<Dictionary<int, int>>.Failure($"Error retrieving counts: {ex.Message}");
            }
        }
        

        public async Task<Result<Dictionary<int, int>>> GetVersesCountByPageAsync()
        {
            try
            {
                _logger.LogDebug("Getting verses count by page");
                var verseRepo = _unitOfWork.Repository<QuranVerse>();
                var counts = await verseRepo.AsQueryable()
                    .GroupBy(v => v.PageNumber)
                    .Select(g => new { PageNumber = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(x => x.PageNumber, x => x.Count);

                return Result<Dictionary<int, int>>.Success(counts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verses count by page");
                return Result<Dictionary<int, int>>.Failure($"Error getting verses count by page: {ex.Message}");
            }
        }

        public async Task<Result<QuranStatisticsDto>> GetQuranStatisticsAsync()
        {
            try
            {
                _logger.LogDebug("Getting Quran statistics");
                var surahs = await _unitOfWork.Quran.GetAllSurahsAsync();
                var versesByJuz = await GetVersesCountByJuzAsync();
                var versesByPage = await GetVersesCountByPageAsync();

                var stats = new QuranStatisticsDto
                {
                    TotalSurahs = surahs.Count(),
                    TotalVerses = await _unitOfWork.Quran.GetTotalVersesCountAsync(),
                    MakkiSurahs = surahs.Count(s => s.RevelationType == RevelationType.Makki),
                    MadaniSurahs = surahs.Count(s => s.RevelationType == RevelationType.Madani),
                    VersesByJuz = versesByJuz.IsSuccess ? versesByJuz.Value! : new Dictionary<int, int>(),
                    VersesByPage = versesByPage.IsSuccess ? versesByPage.Value! : new Dictionary<int, int>()
                };

                return Result<QuranStatisticsDto>.Success(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Quran statistics");
                return Result<QuranStatisticsDto>.Failure($"Error getting Quran statistics: {ex.Message}");
            }
        }
    }
}