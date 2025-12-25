namespace Sukun.Application.Seeder.Hadith_entity
{
    public class ApiBooksResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ApiBook> Books { get; set; } = new();
    }
}

