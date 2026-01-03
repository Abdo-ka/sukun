namespace Sukun.Domin.Entities
{
    public class DuaItem : BaseEntity
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;           
        public string Text { get; set; } = string.Empty;     
        public string? TextEn { get; set; }                
        public string? Reference { get; set; }                   
        public string? Virtue { get; set; }                        
        public int DisplayOrder { get; set; } = 1;

        public virtual DuaCategory Category { get; set; } = null!;
    }
}




