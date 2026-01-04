using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.PrayerTime.Request;
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

        [HttpGet("daily/city/{cityId:guid}")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetDailyByCity(
            Guid cityId,
            [FromQuery] DailyPrayerByCityRequest request)
            => this.ToApiResult(await _prayerTimeService.GetDailyByCityAsync(cityId, request.Date, request.Method));

        [HttpGet("daily/location")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetDailyByLocation(
           [FromQuery] DailyPrayerByLocationRequest request)
            => this.ToApiResult(await _prayerTimeService.GetDailyByLocationAsync(request.Latitude, request.Longitude, request.Date, request.Method));

        [HttpGet("yearly/city/{cityId:guid}")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetYearlyByCity(
            Guid cityId,
            [FromQuery] YearlyPrayerByCityRequest request)
            => this.ToApiResult(await _prayerTimeService.GetYearlyByCityAsync(cityId, request.Year, request.Method));

        [HttpGet("yearly/location")]
        public async Task<ApiResult<PrayerTimeResponseDto>> GetYearlyByLocation([FromQuery] YearlyPrayerByLocationRequest request)
            => this.ToApiResult(await _prayerTimeService.GetYearlyByLocationAsync(request.Latitude, request.Longitude, request.Year, request.Method));
    }

}
