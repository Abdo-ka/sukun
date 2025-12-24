using Microsoft.Extensions.Logging;
using Sukun.Application.Dtos.Mosque.Request;
using Sukun.Application.Dtos.Mosque.Response;
using Sukun.Application.Interfaces;
using Sukun.Application.Mapper;
using Sukun.Domin.Common;
using Sukun.Infrastructure.Abstracts;
using Sukun.Infrastructure.InfrastructureBases;

namespace Sukun.Application.Implemantation
{
    public class MosqueService : IMosqueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMosqueRepository _mosqueRepository;
        private readonly ILogger<MosqueService> _logger;

        public MosqueService(
            IUnitOfWork unitOfWork,
            IMosqueRepository mosqueRepository,
            ILogger<MosqueService> logger)
        {
            _unitOfWork = unitOfWork;
            _mosqueRepository = mosqueRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<MosqueResponseDto>>> GetByCityAsync(Guid cityId)
        {
            var mosques = await _mosqueRepository.GetByCityAsync(cityId);
            return Result<IEnumerable<MosqueResponseDto>>.Success(mosques.Select(m => m.ToResponseDto()));
        }

        public async Task<Result<IEnumerable<MosqueResponseDto>>> GetNearbyAsync(double latitude, double longitude, double radiusInKm = 10)
        {
            var mosques = await _mosqueRepository.GetNearbyAsync(latitude, longitude, radiusInKm);
            return Result<IEnumerable<MosqueResponseDto>>.Success(mosques.Select(m => m.ToResponseDto()));
        }

        public async Task<Result<IEnumerable<MosqueResponseDto>>> GetJummahMosquesAsync(Guid cityId)
        {
            var mosques = await _mosqueRepository.GetJummahMosquesAsync(cityId);
            return Result<IEnumerable<MosqueResponseDto>>.Success(mosques.Select(m => m.ToResponseDto()));
        }

        public async Task<Result<MosqueResponseDto>> GetByIdAsync(Guid id)
        {
            var mosque = await _mosqueRepository.GetByIdAsync(id);
            if (mosque == null || mosque.IsDeleted)
                return Result<MosqueResponseDto>.NotFound("Mosque not found");

            return Result<MosqueResponseDto>.Success(mosque.ToResponseDto());
        }

        // ====================== Admin ======================

        public async Task<Result<MosqueResponseDto>> CreateAsync(MosqueCreateDto dto)
        {
            var mosque = dto.ToEntity();

            var addResult = await _mosqueRepository.AddAsync(mosque);
            if (!addResult.IsSuccess)
                return Result<MosqueResponseDto>.Failure(addResult.Message);

            await _unitOfWork.CompleteAsync();

            return Result<MosqueResponseDto>.Success(mosque.ToResponseDto());
        }

        public async Task<Result<MosqueResponseDto>> UpdateAsync(Guid id, MosqueUpdateDto dto)
        {
            var mosque = await _mosqueRepository.GetByIdAsync(id);
            if (mosque == null)
                return Result<MosqueResponseDto>.NotFound("Mosque not found");

            if (!string.IsNullOrEmpty(dto.Name)) mosque.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.NameAr)) mosque.NameAr = dto.NameAr;
            if (!string.IsNullOrEmpty(dto.Address)) mosque.Address = dto.Address;
            mosque.Latitude = dto.Latitude;
            mosque.Longitude = dto.Longitude;
            if (dto.PhoneNumber != null) mosque.PhoneNumber = dto.PhoneNumber;
            if (dto.Website != null) mosque.Website = dto.Website;
            if (dto.Email != null) mosque.Email = dto.Email;
            mosque.HasPrayerFacilities = dto.HasPrayerFacilities;
            mosque.HasWomenSection = dto.HasWomenSection;
            mosque.HasParking = dto.HasParking;
            mosque.IsJummahMasjid = dto.IsJummahMasjid;
            mosque.IsVerified = dto.IsVerified;

            mosque.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            return Result<MosqueResponseDto>.Success(mosque.ToResponseDto());
        }

        public async Task<Result> SoftDeleteAsync(Guid id)
        {
            var mosque = await _mosqueRepository.GetByIdAsync(id);
            if (mosque == null)
                return Result.NotFound("Mosque not found");

            mosque.IsDeleted = true;
            mosque.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            return Result.Success();
        }
    }
}