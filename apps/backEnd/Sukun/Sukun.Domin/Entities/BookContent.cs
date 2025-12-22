namespace Sukun.Domin.Entities
{
    public class BookContent : BaseEntity
    {
        public Guid SectionId { get; set; }

        public string Title { get; set; } = string.Empty;           // عنوان الفقرة أو الفصل الفرعي
        public string Content { get; set; } = string.Empty;         // النص الطويل (يمكن أن يكون HTML أو Markdown)
        public int DisplayOrder { get; set; } = 1;
        public string? MediaUrl { get; set; }                       // صورة، فيديو، أو صوت توضيحي

        // Navigation
        public virtual IslamicBookSection Section { get; set; } = null!;
    }
}




