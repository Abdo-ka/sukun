namespace Sukun.Application.Seeder.Dua_entity
{
    public class ApiDuaItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Count { get; set; }
        public string? Audio { get; set; }
        public string? Filename { get; set; }
    }
}
