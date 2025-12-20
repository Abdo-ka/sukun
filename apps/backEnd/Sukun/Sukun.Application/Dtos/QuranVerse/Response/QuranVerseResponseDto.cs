namespace Sukun.Application.Dtos.QuranVerse.Response
{
    public class QuranVerseResponseDto
    {
        public Guid Id { get; set; }
        public Guid SurahId { get; set; }
        public int VerseNumber { get; set; }
        public int PageNumber { get; set; }
        public int JuzNumber { get; set; }
        public int HizbNumber { get; set; }
        public string Text { get; set; }
        public string SurahName { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

