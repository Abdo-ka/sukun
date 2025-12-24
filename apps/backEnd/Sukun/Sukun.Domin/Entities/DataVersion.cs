namespace Sukun.Domin.Entities
{
    public class DataVersion : BaseEntity
    {
        public string TableName { get; set; } = string.Empty;  // مثل "Hadiths", "Cities", "Mosques"
        public long Version { get; set; } = 1;                  // يبدأ من 1 ويزيد بـ 1 عند كل تغيير
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}




