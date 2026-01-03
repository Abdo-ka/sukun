using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class RemembranceContent : BaseEntity
    {
        public Guid RemembranceId { get; set; }

        public SourceType SourceType { get; set; }

        public Guid? SourceId { get; set; } 

        public string? CustomContent { get; set; }  
        public int DisplayOrder { get; set; } = 0;

        public virtual Remembrance Remembrance { get; set; } = null!;
    }
}




