namespace Sukun.Domin.Entities
{
    public class Narrative : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public Guid? ParentId { get; set; } 
        public string? CoverImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; } = 0;

        public virtual Narrative? Parent { get; set; }
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<Narrative> Children { get; set; } = new List<Narrative>();
        public virtual ICollection<NarrativeSection> Sections { get; set; } = new List<NarrativeSection>();
    }
}




