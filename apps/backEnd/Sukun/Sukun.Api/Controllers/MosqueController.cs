using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.Mosque.Request;
using Sukun.Application.Dtos.Mosque.Response;
using Sukun.Application.Interfaces;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/mosques")]
    public class MosqueController : ControllerBase
    {
        private readonly IMosqueService _mosqueService;

        public MosqueController(IMosqueService mosqueService)
        {
            _mosqueService = mosqueService;
        }

        // GET: api/mosques/city/{cityId:guid}
        [HttpGet("city/{cityId:guid}")]
        public async Task<ApiResult<IEnumerable<MosqueResponseDto>>> GetByCity(Guid cityId)
            => this.ToApiResult(await _mosqueService.GetByCityAsync(cityId));

        // GET: api/mosques/nearby?latitude=24.7136&longitude=46.6753&radius=10
        [HttpGet("nearby")]
        public async Task<ApiResult<IEnumerable<MosqueResponseDto>>> GetNearby(
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] double radiusInKm = 10)
            => this.ToApiResult(await _mosqueService.GetNearbyAsync(latitude, longitude, radiusInKm));

        // GET: api/mosques/jummah/{cityId:guid}
        [HttpGet("jummah/{cityId:guid}")]
        public async Task<ApiResult<IEnumerable<MosqueResponseDto>>> GetJummahMosques(Guid cityId)
            => this.ToApiResult(await _mosqueService.GetJummahMosquesAsync(cityId));

        // GET: api/mosques/{id:guid}
        [HttpGet("{id:guid}")]
        public async Task<ApiResult<MosqueResponseDto>> GetById(Guid id)
            => this.ToApiResult(await _mosqueService.GetByIdAsync(id));

        // ====================== Admin Operations ======================

        // POST: api/mosques
        [HttpPost]
        public async Task<ApiResult<MosqueResponseDto>> Create([FromBody] MosqueCreateDto dto)
            => this.ToApiResult(await _mosqueService.CreateAsync(dto));

        // PUT: api/mosques/{id:guid}
        [HttpPut("{id:guid}")]
        public async Task<ApiResult<MosqueResponseDto>> Update(Guid id, [FromBody] MosqueUpdateDto dto)
            => this.ToApiResult(await _mosqueService.UpdateAsync(id, dto));

        // DELETE: api/mosques/{id:guid} (Soft Delete)
        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> SoftDelete(Guid id)
            => this.ToApiResult(await _mosqueService.SoftDeleteAsync(id));
    }

}
