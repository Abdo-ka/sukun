using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Tag : BaseEntity
        {
            public string Name { get; set; } = string.Empty;
            public string NameAr { get; set; } = string.Empty;
            public virtual ICollection<NarrativeTags> NarrativeTags { get; set; } = new List<NarrativeTags>();
    }
}




