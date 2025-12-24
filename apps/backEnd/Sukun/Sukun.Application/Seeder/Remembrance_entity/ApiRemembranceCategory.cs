namespace Sukun.Application.Seeder.Remembrance_entity
{
    // Classes للـ Deserialize
    public class ApiRemembranceCategory
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<ApiRemembranceItem>? Array { get; set; }
    }
}
