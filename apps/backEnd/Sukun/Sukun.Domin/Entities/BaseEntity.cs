namespace Sukun.Domin.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long Version { get; set; } = 1; 
        public bool IsDeleted { get; set; }
    }
}




