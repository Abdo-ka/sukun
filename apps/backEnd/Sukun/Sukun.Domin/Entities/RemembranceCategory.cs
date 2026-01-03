using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class RemembranceCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty; 
        public string? NameEn { get; set; }
        public virtual ICollection<RemembranceCategoryLinks> RemembranceCategoryLinks { get; set; } = new List<RemembranceCategoryLinks>();

    }
}




