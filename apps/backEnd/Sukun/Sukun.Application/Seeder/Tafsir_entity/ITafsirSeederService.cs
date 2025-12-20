using Sukun.Domin.Enums;

namespace Sukun.Application.Seeder.Tafsir_entity
{
    public interface ITafsirSeederService
    {
        Task SeedTafsirAsync(TafsirSource source = TafsirSource.Jalalayn);
    }
}
