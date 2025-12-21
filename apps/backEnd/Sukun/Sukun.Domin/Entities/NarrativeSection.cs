namespace Sukun.Domin.Entities
{
    public class NarrativeSection : BaseEntity
    {
        public Guid NarrativeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public string? MediaUrl { get; set; } // صورة أو فيديو أو صوت

        public virtual Narrative Narrative { get; set; } = null!;
    }
}




