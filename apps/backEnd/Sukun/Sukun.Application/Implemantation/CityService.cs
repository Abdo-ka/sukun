using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Sukun.Application.Dtos.City.Request;
using Sukun.Application.Dtos.City.Response;
using Sukun.Application.Dtos.User_Entity.Request;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Domin.Helping;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{

    public class CityService : BaseService, ICityService
    {


        public CityService(IUnitOfWork unitOfWork, ILogger<CityService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<Result<CityResponseDto>> GetByIdAsync(Guid cityId)
        {
            try
            {
                _logger.LogDebug("Getting city by ID: {CityId}", cityId);
                var city = await _unitOfWork.Cities.GetByIdAsync(cityId);
                if (city == null)
                    return Result<CityResponseDto>.NotFound($"City with ID {cityId} not found");

                return Result<CityResponseDto>.Success(CityMapper.ToResponseDto(city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting city by ID: {CityId}", cityId);
                return Result<CityResponseDto>.Failure($"Error retrieving city: {ex.Message}");
            }
        }

        public async Task<Result<CityResponseDto>> GetByNameAndCountryAsync(string name, string countryCode)
        {
            try
            {
                _logger.LogDebug("Getting city by name and country: {Name}, {CountryCode}", name, countryCode);
                var city = await _unitOfWork.Cities.GetByNameAndCountryAsync(name, countryCode);
                if (city == null)
                    return Result<CityResponseDto>.NotFound($"City {name}, {countryCode} not found");

                return Result<CityResponseDto>.Success(CityMapper.ToResponseDto(city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting city by name and country");
                return Result<CityResponseDto>.Failure($"Error retrieving city: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CityResponseDto>>> GetCitiesByCountryAsync(string countryCode)
        {
            try
            {
                _logger.LogDebug("Getting cities by country: {CountryCode}", countryCode);
                var cities = await _unitOfWork.Cities.GetCitiesByCountryAsync(countryCode);
                return Result<IEnumerable<CityResponseDto>>.Success(cities.Select(x => CityMapper.ToResponseDto(x)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities by country: {CountryCode}", countryCode);
                return Result<IEnumerable<CityResponseDto>>.Failure($"Error retrieving cities: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<CityResponseDto>>> SearchCitiesAsync(string searchTerm)
        {
            try
            {
                _logger.LogDebug("Searching cities with term: {SearchTerm}", searchTerm);
                var cities = await _unitOfWork.Cities.SearchCitiesAsync(searchTerm);
                return Result<IEnumerable<CityResponseDto>>.Success(cities.Select(CityMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching cities: {SearchTerm}", searchTerm);
                return Result<IEnumerable<CityResponseDto>>.Failure($"Error searching cities: {ex.Message}");
            }
        }


        public async Task<Result<IEnumerable<CityResponseDto>>> GetCitiesByTimeZoneAsync(int timeZone)
        {
            try
            {
                _logger.LogDebug("Getting cities by timezone: {TimeZone}", timeZone);
                var cities = await _unitOfWork.Cities.GetCitiesByTimeZoneAsync(timeZone);
                return Result<IEnumerable<CityResponseDto>>.Success(cities.Select(CityMapper.ToResponseDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities by timezone: {TimeZone}", timeZone);
                return Result<IEnumerable<CityResponseDto>>.Failure($"Error retrieving cities: {ex.Message}");
            }
        }

        public async Task<Result<CityResponseDto>> FindNearestCityAsync(double latitude, double longitude)
        {
            try
            {
                _logger.LogDebug("Finding nearest city for coordinates: {Latitude}, {Longitude}", latitude, longitude);
                var city = await _unitOfWork.Cities.FindNearestCityAsync(latitude, longitude);
                if (city == null)
                    return Result<CityResponseDto>.NotFound("No city found near the provided coordinates");

                return Result<CityResponseDto>.Success(CityMapper.ToResponseDto(city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding nearest city");
                return Result<CityResponseDto>.Failure($"Error finding nearest city: {ex.Message}");
            }
        }

        public async Task<Result<int>> CountCitiesByCountryAsync(string countryCode)
        {
            try
            {
                _logger.LogDebug("Counting cities by country: {CountryCode}", countryCode);
                var count = await _unitOfWork.Cities.CountCitiesByCountryAsync(countryCode);
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting cities by country: {CountryCode}", countryCode);
                return Result<int>.Failure($"Error counting cities: {ex.Message}");
            }
        }

        public async Task<Result<CityResponseDto>> UpdateCoordinatesAsync(Guid cityId, double latitude, double longitude)
        {
            try
            {
                _logger.LogDebug("Updating coordinates for city: {CityId}", cityId);
                var result = await _unitOfWork.Cities.UpdateCoordinatesAsync(cityId, latitude, longitude);
                if (!result.IsSuccess)
                    return Result<CityResponseDto>.NotFound(result.Message);

                await _unitOfWork.CompleteAsync();
                return Result<CityResponseDto>.Success(CityMapper.ToResponseDto(result.Value));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating coordinates for city: {CityId}", cityId);
                return Result<CityResponseDto>.Failure($"Error updating coordinates: {ex.Message}");
            }
        }
        public async Task<Result<CityResponseDto>> CreateCityAsync(CityCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Creating new city: {Name}, {Country}", dto.Name, dto.Country);
                var existingCity = await _unitOfWork.Cities.GetByNameAndCountryAsync(dto.Name, dto.CountryCode);
                if (existingCity != null)
                    return Result<CityResponseDto>.Failure($"City {dto.Name} in {dto.Country} already exists");

                var city = CityMapper.FromCreateDto(dto);
                await _unitOfWork.Cities.AddAsync(city);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("City created successfully with ID: {CityId}", city.Id);
                return Result<CityResponseDto>.Success(CityMapper.ToResponseDto(city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating city: {Name}, {Country}", dto.Name, dto.Country);
                return Result<CityResponseDto>.Failure($"Error creating city: {ex.Message}");
            }
        }

        public async Task<Result<PagedResponseDto<CityResponseDto>>> GetCitiesAsync(PagedRequestDto request)
        {
            try
            {
                _logger.LogDebug("Getting cities page {PageNumber} with size {PageSize}", request.PageNumber, request.PageSize);

                var query = _unitOfWork.Cities.AsQueryable();
                if(!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = query.Where(b => b.Name == request.SearchTerm || b.NameAr == request.SearchTerm);
                }
                var totalCount = query.Count();
               
                query = query.ApplyPaginatedAsync(request.PageNumber,request.PageSize);
                var pagedCitiesDto = query.Select(CityMapper.ToResponseDto).ToList();
                var response = CreatePagedResponse(pagedCitiesDto, request.PageNumber, request.PageSize, totalCount);
                return Result<PagedResponseDto<CityResponseDto>>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cities");
                return Result<PagedResponseDto<CityResponseDto>>.Failure($"Error getting cities: {ex.Message}");
            }
        }

        public async Task<Result<CityResponseDto>> UpdateCityAsync(Guid cityId, CityUpdateDto dto)
        {
            try
            {
                _logger.LogInformation("Updating city with ID: {CityId}", cityId);
                var city = await _unitOfWork.Cities.GetByIdAsync(cityId);
                if (city == null)
                    return Result<CityResponseDto>.NotFound($"City with ID {cityId} not found");

                CityMapper.UpdateFromDto(city, dto);
                await _unitOfWork.Cities.UpdateAsync(city);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("City updated successfully with ID: {CityId}", cityId);
                return Result<CityResponseDto>.Success(CityMapper.ToResponseDto(city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating city with ID: {CityId}", cityId);
                return Result<CityResponseDto>.Failure($"Error updating city: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteCityAsync(Guid cityId)
        {
            try
            {
                _logger.LogInformation("Deleting city with ID: {CityId}", cityId);
                var city = await _unitOfWork.Cities.GetByIdAsync(cityId);
                if (city == null)
                    return Result<bool>.NotFound($"City with ID {cityId} not found");

                await _unitOfWork.Cities.DeleteAsync(city);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("City deleted successfully with ID: {CityId}", cityId);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting city with ID: {CityId}", cityId);
                return Result<bool>.Failure($"Error deleting city: {ex.Message}");
            }
        }

        //public async Task<Result<PagedResponseDto<CityResponseDto>>> SearchCitiesAsync(SearchRequestDto request)
        //{
        //    try
        //    {
        //        _logger.LogDebug("Searching cities with term: {SearchTerm}", request.SearchTerm);
        //        IEnumerable<City> cities;
        //        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        //            cities = await _unitOfWork.Cities.SearchCitiesAsync(request.SearchTerm);
        //        else
        //            cities = await _unitOfWork.Cities.GetAllAsync();

        //        if (!string.IsNullOrWhiteSpace(request.CountryCode))
        //            cities = cities.Where(c => c.CountryCode == request.CountryCode);

        //        var totalCount = cities.Count();
        //        var pagedCities = cities
        //            .Skip((request.PageNumber - 1) * request.PageSize)
        //            .Take(request.PageSize)
        //            .Select(CityMapper.ToResponseDto)
        //            .ToList();

        //        var response = CreatePagedResponse(pagedCities, request.PageNumber, request.PageSize, totalCount);
        //        return Result<PagedResponseDto<CityResponseDto>>.Success(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error searching cities");
        //        return Result<PagedResponseDto<CityResponseDto>>.Failure($"Error searching cities: {ex.Message}");
        //    }
        //}

        public async Task<Result<int>> GetCityCountByCountryAsync(string countryCode)
        {
            try
            {
                _logger.LogDebug("Getting city count by country: {CountryCode}", countryCode);
                var count = await _unitOfWork.Cities.CountCitiesByCountryAsync(countryCode);
                return Result<int>.Success(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting city count by country: {CountryCode}", countryCode);
                return Result<int>.Failure($"Error getting city count: {ex.Message}");
            }
        }
    }
}