using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.PrayerTime.Response;
using Sukun.Application.Interfaces;
using Sukun.Domin.Common;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;
using Sukun.Infrastructure.InfrastructureBases;
using System.Net.Http.Json;

namespace Sukun.Application.Implemantation
{
    public class PrayerTimeService : IPrayerTimeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PrayerTimeService> _logger;

        public PrayerTimeService(IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork, ILogger<PrayerTimeService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // 1. يوم واحد عن طريق cityId
        public async Task<Result<PrayerTimeResponseDto>> GetDailyByCityAsync(Guid cityId, DateTime? date = null, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague)
        {
            var city = await _unitOfWork.Repository<City>().GetByIdAsync(cityId);
            if (city == null)
                return Result<PrayerTimeResponseDto>.NotFound("المدينة غير موجودة");

            return await GetPrayerTimesAsync(city.Latitude, city.Longitude, date, method, city.NameAr);
        }

        // 2. يوم واحد عن طريق الإحداثيات
        public async Task<Result<PrayerTimeResponseDto>> GetDailyByLocationAsync(double latitude, double longitude, DateTime? date = null, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague, string? cityName = null)
        {
            return await GetPrayerTimesAsync(latitude, longitude, date, method, cityName);
        }

        // 3. سنة كاملة عن طريق cityId
        public async Task<Result<PrayerTimeResponseDto>> GetYearlyByCityAsync(Guid cityId, int year, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague)
        {
            var city = await _unitOfWork.Repository<City>().GetByIdAsync(cityId);
            if (city == null)
                return Result<PrayerTimeResponseDto>.NotFound("المدينة غير موجودة");

            return await GetYearlyPrayerTimesAsync(city.Latitude, city.Longitude, year, method, city.NameAr);
        }

        // 4. سنة كاملة عن طريق الإحداثيات
        public async Task<Result<PrayerTimeResponseDto>> GetYearlyByLocationAsync(double latitude, double longitude, int year, PrayerCalculationMethod method = PrayerCalculationMethod.MuslimWorldLeague, string? cityName = null)
        {
            return await GetYearlyPrayerTimesAsync(latitude, longitude, year, method, cityName);
        }

        // دالة مشتركة ليوم واحد
        private async Task<Result<PrayerTimeResponseDto>> GetPrayerTimesAsync(double latitude, double longitude, DateTime? date, PrayerCalculationMethod method, string? cityName)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var methodId = (int)method;
                var dateStr = (date ?? DateTime.Today).ToString("dd-MM-yyyy");

                var url = $"http://api.aladhan.com/v1/timings/{dateStr}?latitude={latitude}&longitude={longitude}&method={methodId}";

                var response = await client.GetFromJsonAsync<AladhanApiResponse>(url);

                if (response == null || response.Code != 200 || response.Data == null)
                    return Result<PrayerTimeResponseDto>.Failure("فشل جلب أوقات الصلاة");

                var timings = response.Data.Timings;
                var hijriDate = $"{response.Data.Date.Hijri.Day} {response.Data.Date.Hijri.Month.Ar} {response.Data.Date.Hijri.Year}";

                var dto = new PrayerTimeResponseDto
                {
                    CityName = cityName ?? "موقع مخصص",
                    DailyTimes = new List<DailyPrayerTimeDto>
                    {
                        new DailyPrayerTimeDto
                        {
                            GregorianDate = response.Data.Date.Readable,
                            HijriDate = hijriDate,
                            Fajr = timings.Fajr,
                            Sunrise = timings.Sunrise,
                            Dhuhr = timings.Dhuhr,
                            Asr = timings.Asr,
                            Maghrib = timings.Maghrib,
                            Isha = timings.Isha
                        }
                    }
                };

                CalculateNextPrayer(dto);
                return Result<PrayerTimeResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching daily prayer times");
                return Result<PrayerTimeResponseDto>.Failure("حدث خطأ");
            }
        }

        // دالة مشتركة لسنة كاملة
        private async Task<Result<PrayerTimeResponseDto>> GetYearlyPrayerTimesAsync(double latitude, double longitude, int year, PrayerCalculationMethod method, string? cityName)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var methodId = (int)method;
                var dto = new PrayerTimeResponseDto { CityName = cityName ?? "موقع مخصص" };

                for (int month = 1; month <= 12; month++)
                {
                    var url = $"http://api.aladhan.com/v1/calendar/{year}/{month}?latitude={latitude}&longitude={longitude}&method={methodId}";

                    var response = await client.GetFromJsonAsync<AladhanCalendarResponse>(url);
                    if (response == null || response.Code != 200 || response.Data == null) continue;

                    foreach (var day in response.Data)
                    {
                        var hijriDate = $"{day.Date.Hijri.Day} {day.Date.Hijri.Month.Ar} {day.Date.Hijri.Year}";

                        dto.DailyTimes.Add(new DailyPrayerTimeDto
                        {
                            GregorianDate = day.Date.Readable,
                            HijriDate = hijriDate,
                            Fajr = day.Timings.Fajr,
                            Sunrise = day.Timings.Sunrise,
                            Dhuhr = day.Timings.Dhuhr,
                            Asr = day.Timings.Asr,
                            Maghrib = day.Timings.Maghrib,
                            Isha = day.Timings.Isha
                        });
                    }
                }

                dto.DailyTimes = dto.DailyTimes.OrderBy(d => d.GregorianDate).ToList();
                return Result<PrayerTimeResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching yearly prayer times");
                return Result<PrayerTimeResponseDto>.Failure("حدث خطأ");
            }
        }

        private void CalculateNextPrayer(PrayerTimeResponseDto dto)
        {
            if (dto.DailyTimes.Count == 0) return;

            var today = dto.DailyTimes[0];
            var now = DateTime.Now.TimeOfDay;

            var prayers = new List<(string name, TimeSpan time)>
            {
                ("الفجر", TimeSpan.Parse(today.Fajr)),
                ("الظهر", TimeSpan.Parse(today.Dhuhr)),
                ("العصر", TimeSpan.Parse(today.Asr)),
                ("المغرب", TimeSpan.Parse(today.Maghrib)),
                ("العشاء", TimeSpan.Parse(today.Isha))
            };

            var next = prayers.FirstOrDefault(p => p.time > now);
            if (next.name == default)
            {
                next = prayers[0];
                dto.NextPrayer = "الفجر (غدًا)";
            }
            else
            {
                dto.NextPrayer = next.name;
            }

            var timeUntil = next.time - now;
            if (timeUntil.TotalSeconds < 0) timeUntil = timeUntil.Add(TimeSpan.FromDays(1));

            dto.TimeUntilNextPrayer = FormatTimeSpan(timeUntil);
        }

        private string FormatTimeSpan(TimeSpan ts)
        {
            if (ts.TotalHours >= 1)
                return $"بعد {(int)ts.TotalHours} ساعة و {ts.Minutes} دقيقة";
            return $"بعد {ts.Minutes} دقيقة";
        }
    }



}