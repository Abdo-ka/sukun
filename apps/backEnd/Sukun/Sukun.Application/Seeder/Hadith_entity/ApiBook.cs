namespace Sukun.Application.Seeder.Hadith_entity
{
    public class ApiBook
    {
        public int Id { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string WriterName { get; set; } = string.Empty;
        public string? AboutWriter { get; set; }
        public string WriterDeath { get; set; } = string.Empty;
        public string BookSlug { get; set; } = string.Empty;
        public string Hadiths_count { get; set; } = string.Empty;
        public string Chapters_count { get; set; } = string.Empty;
    }
}

