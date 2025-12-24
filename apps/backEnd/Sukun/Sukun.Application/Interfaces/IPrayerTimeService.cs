using Sukun.Application.Dtos.PrayerTime.Response;
using Sukun.Domin.Common;
using Sukun.Domin.Enums;

namespace Sukun.Application.Interfaces
{
    public interface IPrayerTimeService
    {
        Task<Result<PrayerTimeResponseDto>> GetDailyByCityAsync(Guid cityId, DateTime? date = null, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague);
        Task<Result<PrayerTimeResponseDto>> GetDailyByLocationAsync(double latitude, double longitude, DateTime? date = null, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague, string? cityName = null);

        Task<Result<PrayerTimeResponseDto>> GetYearlyByCityAsync(Guid cityId, int year, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague);
        Task<Result<PrayerTimeResponseDto>> GetYearlyByLocationAsync(double latitude, double longitude, int year, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague, string? cityName = null);
    }



}