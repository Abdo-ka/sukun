using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.QuranSurah.Response;
using Sukun.Application.Dtos.QuranVerse.Response;
using Sukun.Application.Dtos.Tafsir.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Seeder.Quran;
using Sukun.Application.Seeder.Tafsir_entity;
using Sukun.Domin.Common;
using Sukun.Domin.Enums;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/quran")]
    public class QuranController : ControllerBase
    {
        private readonly IQuranService _quranService;

        public IQuranSeederService _quranSeederService { get; }
        public ITafsirSeederService _tafsirSeederService { get; }

        public QuranController(IQuranService quranService, IQuranSeederService quranSeederService , ITafsirSeederService tafsirSeederService)
        {
            _quranService = quranService;
            _quranSeederService = quranSeederService;
            _tafsirSeederService = tafsirSeederService;
        }

        // Surah
        [HttpGet("surah/{surahNumber:int}")]
        public async Task<ApiResult<QuranSurahResponseDto>> GetSurah(int surahNumber)
            => this.ToApiResult(await _quranService.GetSurahByNumberAsync(surahNumber));

        [HttpGet("surah/{surahNumber:int}/detail")]
        public async Task<ApiResult<QuranSurahWithVersesResponseDto>> GetSurahWithVerses(int surahNumber)
            => this.ToApiResult(await _quranService.GetSurahWithVersesAsync(surahNumber));

        [HttpGet("surahs")]
        public async Task<ApiResult<IEnumerable<QuranSurahResponseDto>>> GetAllSurahs()
            => this.ToApiResult(await _quranService.GetAllSurahsAsync());

        [HttpGet("surahs/revelation/{type}")]
        public async Task<ApiResult<IEnumerable<QuranSurahResponseDto>>> GetSurahsByType(RevelationType type)
            => this.ToApiResult(await _quranService.GetSurahsByRevelationTypeAsync(type));

        // Verse
        [HttpGet("verse/{verseId:guid}")]
        public async Task<ApiResult<QuranVerseResponseDto>> GetVerse(Guid verseId)
            => this.ToApiResult(await _quranService.GetVerseByIdAsync(verseId));

        [HttpGet("verse/{verseId:guid}/detail")]
        public async Task<ApiResult<QuranVerseWithDetailsResponseDto>> GetVerseDetail(Guid verseId)
            => this.ToApiResult(await _quranService.GetVerseWithDetailsAsync(verseId));

        [HttpGet("verse/{surahNumber:int}/{verseNumber:int}")]
        public async Task<ApiResult<QuranVerseResponseDto>> GetVerseByNumber(int surahNumber, int verseNumber)
            => this.ToApiResult(await _quranService.GetVerseBySurahAndNumberAsync(surahNumber, verseNumber));

        //[HttpGet("verses/search")]
        //public async Task<ApiResult<PagedResponseDto<VerseResponseDto>>> SearchVerses([FromQuery] SearchRequestDto request)
        //    => this.ToApiResult(await _quranService.SearchVersesAsync(request));

        [HttpGet("verse/random")]
        public async Task<ApiResult<QuranVerseResponseDto>> GetRandomVerse()
            => this.ToApiResult(await _quranService.GetRandomVerseAsync());

        // Tafsir
        [HttpGet("tafsir/verse/{verseId:guid}")]
        public async Task<ApiResult<IEnumerable<TafsirResponseDto>>> GetTafsirs(Guid verseId)
            => this.ToApiResult(await _quranService.GetTafsirsByVerseAsync(verseId));

        [HttpGet("tafsir/verse/{verseId:guid}/source/{source}")]
        public async Task<ApiResult<TafsirResponseDto>> GetTafsirBySource(Guid verseId, TafsirSource source)
            => this.ToApiResult(await _quranService.GetTafsirByVerseAndSourceAsync(verseId, source));

        // Statistics
        [HttpGet("stats")]
        public async Task<ApiResult<QuranStatisticsDto>> GetStatistics()
            => this.ToApiResult(await _quranService.GetQuranStatisticsAsync());

        [HttpGet("stats/verses/juz")]
        public async Task<ApiResult<Dictionary<int, int>>> GetVersesByJuz()
            => this.ToApiResult(await _quranService.GetVersesCountByJuzAsync());

        [HttpGet("stats/verses/page")]
        public async Task<ApiResult<Dictionary<int, int>>> GetVersesByPage()
            => this.ToApiResult(await _quranService.GetVersesCountByPageAsync());
    
        [HttpPost("seed-quran")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ApiResult> SeedQuran()
        {
            var surahsCount = await _quranService.GetTotalSurahCountAsync();
            if (!surahsCount.IsSuccess)
                return this.ToApiResult(Result.Failure(surahsCount.Message ?? "Error checking Quran data"));

            if (surahsCount.Value > 0)
                return ApiResult.Ok("Quran already seeded, skipping...");

            try
            {
                await _quranSeederService.SeedQuranAsync();
                return ApiResult.Ok("Quran seeded successfully");
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Error seeding Quran");
                return ApiResult.InternalServerError("Failed to seed Quran data");
            }
        }

        [HttpPost("seed-tafsir")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ApiResult> SeedTafsir([FromQuery] TafsirSource source = TafsirSource.Jalalayn)
        {
            var existingCount = await _quranService.GetTafsirCountBySourceAsync(source);
            if (!existingCount.IsSuccess)
                return this.ToApiResult(Result.Failure("Error checking tafsir data"));

            if (existingCount.Value > 0)
                return ApiResult.Ok($"Tafsir {source} already seeded, skipping...");

            try
            {
                await _tafsirSeederService.SeedTafsirAsync(source);
                return ApiResult.Ok($"Tafsir {source} seeded successfully");
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Error seeding tafsir {Source}", source);
                return ApiResult.InternalServerError($"Failed to seed tafsir {source}");
            }
        }
    }
 
}
