namespace Sukun.Domin.Entities
{
    public class RemembranceCategoryLinks : BaseEntity
    {
        public Guid RemembranceId { get; set; }
        public Guid RemembranceCategoryId { get; set; }
        public Remembrance Remembrance { get; set; }
        public RemembranceCategory RemembranceCategory { get; set; }
    }
}




