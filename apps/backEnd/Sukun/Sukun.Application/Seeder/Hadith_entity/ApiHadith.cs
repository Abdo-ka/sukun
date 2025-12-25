namespace Sukun.Application.Seeder.Hadith_entity
{
    public class ApiHadith
    {
        public int Id { get; set; }
        public string HadithNumber { get; set; } 
        public string EnglishNarrator { get; set; } = string.Empty;
        public string HadithEnglish { get; set; } = string.Empty;
        public string HadithUrdu { get; set; } = string.Empty;
        public string UrduNarrator { get; set; } = string.Empty;
        public string HadithArabic { get; set; } = string.Empty;
        public string HeadingArabic { get; set; } = string.Empty;
        public string HeadingUrdu { get; set; } = string.Empty;
        public string HeadingEnglish { get; set; } = string.Empty;
        public string ChapterId { get; set; } = string.Empty;
        public string BookSlug { get; set; } = string.Empty;
        public string Volume { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public ApiBookRef Book { get; set; } = null!;
        public ApiChapterRef Chapter { get; set; } = null!;
    }
}

