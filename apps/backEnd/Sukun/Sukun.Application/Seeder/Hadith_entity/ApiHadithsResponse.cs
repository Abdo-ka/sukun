namespace Sukun.Application.Seeder.Hadith_entity
{
    public class ApiHadithsResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public ApiHadithsData Hadiths { get; set; } = null!;
    }
}

