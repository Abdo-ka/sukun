namespace Sukun.Domin.Entities
{
    public class Remembrance : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        public int RecommendedCount { get; set; } = 1;
        public string? Benefits { get; set; }

        public bool IsDaily { get; set; } = false;

        public virtual ICollection<RemembranceContent> Contents { get; set; } = new List<RemembranceContent>();
        public virtual ICollection<RemembranceCategoryLinks> RemembranceCategoryLinks { get; set; } = new List<RemembranceCategoryLinks>();
    }
}




