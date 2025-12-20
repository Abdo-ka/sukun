using Sukun.Application.Dtos.City.Request;
using Sukun.Application.Dtos.City.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Domin.Common;

namespace Sukun.Application.Interfaces
{
    public interface ICityService
    {
        Task<Result<CityResponseDto>> GetByIdAsync(Guid cityId);
        Task<Result<CityResponseDto>> GetByNameAndCountryAsync(string name, string countryCode);
        Task<Result<IEnumerable<CityResponseDto>>> GetCitiesByCountryAsync(string countryCode);
        Task<Result<IEnumerable<CityResponseDto>>> SearchCitiesAsync(string searchTerm);
        Task<Result<IEnumerable<CityResponseDto>>> GetCitiesByTimeZoneAsync(int timeZone);
        Task<Result<CityResponseDto>> FindNearestCityAsync(double latitude, double longitude);
        Task<Result<int>> CountCitiesByCountryAsync(string countryCode);
        Task<Result<CityResponseDto>> UpdateCoordinatesAsync(Guid cityId, double latitude, double longitude);
        Task<Result<CityResponseDto>> CreateCityAsync(CityCreateDto dto);
        Task<Result<PagedResponseDto<CityResponseDto>>> GetCitiesAsync(PagedRequestDto request);
        Task<Result<CityResponseDto>> UpdateCityAsync(Guid cityId, CityUpdateDto dto);
        Task<Result<bool>> DeleteCityAsync(Guid cityId);
        Task<Result<int>> GetCityCountByCountryAsync(string countryCode);
    }
}