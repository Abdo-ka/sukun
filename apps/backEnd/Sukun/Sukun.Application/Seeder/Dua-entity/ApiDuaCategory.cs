namespace Sukun.Application.Seeder.Dua_entity
{
    // Classes للـ Deserialize من JSON المصدر
    public class ApiDuaCategory
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? Audio { get; set; }
        public string? Filename { get; set; }
        public List<ApiDuaItem>? Array { get; set; }
    }
}
