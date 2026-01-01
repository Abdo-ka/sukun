namespace Sukun.Domin.Entities
{
    public class NarrativeTags : BaseEntity
    {
        public Guid NarrativeId {  get; set; }
        public Guid TagId { get; set; }
        public Narrative Narrative { get; set; }
        public Tag Tag { get; set; }
    }

}




