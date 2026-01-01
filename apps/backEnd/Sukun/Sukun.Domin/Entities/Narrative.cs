using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Narrative : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public ContentType Type { get; set; }
        public string? ShortDescription { get; set; }
        public bool IsFeatured { get; set; }
        public int ViewCount { get; set; } = 0;

        public virtual ICollection<NarrativeTags> NarrativeTags { get; set; } = new List<NarrativeTags>();
        public virtual ICollection<NarrativeCategory> NarrativeCategories { get; set; } = new List<NarrativeCategory>();
        public virtual ICollection<NarrativeSection> Sections { get; set; } = new List<NarrativeSection>();
    }

}




