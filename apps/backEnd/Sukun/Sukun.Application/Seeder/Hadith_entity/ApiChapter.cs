namespace Sukun.Application.Seeder.Hadith_entity
{
    public class ApiChapter
    {
        public int Id { get; set; }
        public int ChapterNumber { get; set; } 
        public string ChapterEnglish { get; set; } = string.Empty;
        public string ChapterUrdu { get; set; } = string.Empty;
        public string ChapterArabic { get; set; } = string.Empty;
        public string BookSlug { get; set; } = string.Empty;
    }
}

