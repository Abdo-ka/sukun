namespace Sukun.Application.Seeder.Quran.dto
{
    public class QuranApiResponse
    {
        public int Code { get; set; }
        public string Status { get; set; } = string.Empty;
        public QuranData Data { get; set; } = new();
    }
}
