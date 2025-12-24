namespace Sukun.Application.Seeder.Remembrance_entity
{
    // Classes للـ Deserialize (تعدل حسب الهيكل الفعلي للـ JSON)
    public class ApiMorningEveningData
        {
            public List<ApiDailyAdhkarItem> Morning { get; set; } = new();
            public List<ApiDailyAdhkarItem> Evening { get; set; } = new();
        }
    }
   

