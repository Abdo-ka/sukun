using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sukun.Api.Bases;
using Sukun.Api.Extention;
using Sukun.Application.Dtos.AsmaulHusna.Responce;
using Sukun.Application.Interfaces;
using Sukun.Application.Seeder.AsumalHausna_entity;

namespace Sukun.Api.Controllers
{
    [ApiController]
    [Route("api/asmaul-husna")]
    public class AsmaulHusnaController : ControllerBase
    {
        private readonly IAsmaulHusnaService _asmaulHusnaService;

        public AsmaulHusnaController(IAsmaulHusnaService asmaulHusnaService , IAsmaulHusnaSeederService asmaulHusnaSeederService)
        {
            _asmaulHusnaService = asmaulHusnaService;
            _asmaulHusnaSeederService = asmaulHusnaSeederService;
        }

        public IAsmaulHusnaSeederService _asmaulHusnaSeederService { get; }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<AsmaulHusnaResponseDto>>> GetAll()
            => this.ToApiResult(await _asmaulHusnaService.GetAllAsync());

        [HttpGet("{number:int}")]
        public async Task<ApiResult<AsmaulHusnaResponseDto>> GetByNumber(int number)
            => this.ToApiResult(await _asmaulHusnaService.GetByNumberAsync(number));

        [HttpGet("random")]
        public async Task<ApiResult<AsmaulHusnaResponseDto>> GetRandom()
            => this.ToApiResult(await _asmaulHusnaService.GetRandomAsync());
        [HttpPost("seed-asmaul-husna")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> SeedAsmaulHusna()
        {
            await _asmaulHusnaSeederService.SeedAsmaulHusnaAsync();
            return Ok("Asmaul Husna seeded successfully");
        }
    }
   
}
