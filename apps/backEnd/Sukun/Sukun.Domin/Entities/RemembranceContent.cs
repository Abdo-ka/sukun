using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class RemembranceContent : BaseEntity
    {
        public Guid RemembranceId { get; set; }

        public SourceType SourceType { get; set; }

        public Guid? SourceId { get; set; } // Id المرتبط (آية، حديث، اسم الله، دعاء مخصص...)

        public string? CustomContent { get; set; } // فقط إذا كان SourceType = Custom

        public virtual Remembrance Remembrance { get; set; } = null!;
    }
}




