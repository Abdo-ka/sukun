namespace Sukun.Application.Seeder.Hadith_entity
{
    public class ApiHadithsData
    {
        public int Current_page { get; set; }
        public List<ApiHadith> Data { get; set; } = new();
        public int Last_page { get; set; }
    }
}

