using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.City.Request;
using Sukun.Application.Dtos.City.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost]
        public async Task<ApiResult<CityResponseDto>> Create([FromBody] CityCreateDto request)
            => this.ToApiResult(await _cityService.CreateCityAsync(request));

        [HttpGet("{cityId:guid}")]
        public async Task<ApiResult<CityResponseDto>> GetById(Guid cityId)
            => this.ToApiResult(await _cityService.GetByIdAsync(cityId));

      
        [HttpGet]
        public async Task<ApiResult<PagedResponseDto<CityResponseDto>>> GetAll([FromQuery] PagedRequestDto request)
            => this.ToApiResult(await _cityService.GetCitiesAsync(request));

        //[HttpGet("search")]
        //public async Task<ApiResult<PagedResponseDto<CityResponseDto>>> Search([FromQuery] SearchRequestDto request)
        //    => this.ToApiResult(await _cityService.SearchCitiesAsync(request));

        [HttpPut("{cityId:guid}")]
        public async Task<ApiResult<CityResponseDto>> Update(Guid cityId, [FromBody] CityUpdateDto request)
            => this.ToApiResult(await _cityService.UpdateCityAsync(cityId, request));

        [HttpDelete("{cityId:guid}")]
        public async Task<ApiResult<bool>> Delete(Guid cityId)
            => this.ToApiResult(await _cityService.DeleteCityAsync(cityId));

        [HttpGet("country/{countryCode}")]
        public async Task<ApiResult<IEnumerable<CityResponseDto>>> GetByCountry(string countryCode)
            => this.ToApiResult(await _cityService.GetCitiesByCountryAsync(countryCode));

        [HttpGet("nearest")]
        public async Task<ApiResult<CityResponseDto>> FindNearest([FromQuery] double latitude, [FromQuery] double longitude)
            => this.ToApiResult(await _cityService.FindNearestCityAsync(latitude, longitude));

        [HttpPatch("{cityId:guid}/coordinates")]
        public async Task<ApiResult<CityResponseDto>> UpdateCoordinates(Guid cityId, [FromQuery] double latitude, [FromQuery] double longitude)
            => this.ToApiResult(await _cityService.UpdateCoordinatesAsync(cityId, latitude, longitude));

           [HttpGet("count/country/{countryCode}")]
        public async Task<ApiResult<int>> GetCountByCountry(string countryCode)
            => this.ToApiResult(await _cityService.GetCityCountByCountryAsync(countryCode));
    }
 
}
