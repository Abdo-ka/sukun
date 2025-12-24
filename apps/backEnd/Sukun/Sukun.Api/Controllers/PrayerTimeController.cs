using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.PrayerTime.Response;
using Sukun.Application.Interfaces;
using Sukun.Domin.Enums;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/prayer-times")]
    public class PrayerTimeController : ControllerBase
    {
        private readonly IPrayerTimeService _prayerTimeService;

        public PrayerTimeController(IPrayerTimeService prayerTimeService)
        {
            _prayerTimeService = prayerTimeService;
        }

        // 1. يوم واحد عن طريق cityId
        [HttpGet("daily/city/{cityId:guid}")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetDailyByCity(
            Guid cityId,
            [FromQuery] DateTime? date = null,
            [FromQuery] PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague)
            => this.ToApiResult(await _prayerTimeService.GetDailyByCityAsync(cityId, date, method));

        // 2. يوم واحد عن طريق الإحداثيات
        [HttpGet("daily/location")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetDailyByLocation(
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] DateTime? date = null,
            [FromQuery] PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague,
            [FromQuery] string? cityName = null)
            => this.ToApiResult(await _prayerTimeService.GetDailyByLocationAsync(latitude, longitude, date, method, cityName));

        // 3. سنة كاملة عن طريق cityId
        [HttpGet("yearly/city/{cityId:guid}")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetYearlyByCity(
            Guid cityId,
            [FromQuery] int year,
            [FromQuery] PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague)
            => this.ToApiResult(await _prayerTimeService.GetYearlyByCityAsync(cityId, year, method));

        // 4. سنة كاملة عن طريق الإحداثيات
        [HttpGet("yearly/location")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetYearlyByLocation(
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] int year,
            [FromQuery] PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague,
            [FromQuery] string? cityName = null)
            => this.ToApiResult(await _prayerTimeService.GetYearlyByLocationAsync(latitude, longitude, year, method, cityName));
    }
}
