namespace Sukun.Application.Seeder.Hadith_entity
{
    public class ApiChaptersResponse
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ApiChapter> Chapters { get; set; } = new();
    }
}

