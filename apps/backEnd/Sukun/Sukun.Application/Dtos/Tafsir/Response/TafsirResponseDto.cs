using Sukun.Domin.Enums;

namespace Sukun.Application.Dtos.Tafsir.Response
{
    // Tafsir DTOs
    public class TafsirResponseDto
    {
        public Guid Id { get; set; }
        public Guid? VerseId { get; set; }
        public TafsirSource Source { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

