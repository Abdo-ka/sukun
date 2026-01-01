namespace Sukun.Domin.Entities
{
    public class NarrativeCategory : BaseEntity
    {
        public Guid NarrativeId { get; set; }
        public Guid CategoryId { get; set; }
        public int DisplayOrder { get; set; } = 0;

        // علاقات
        public Narrative Narrative { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }

}




