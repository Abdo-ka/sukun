using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Tag : BaseEntity
        {
            public NarrativeTag TagType { get; set; }

            public string NameAr { get; set; } = string.Empty;

            public string NameEn { get; set; } = string.Empty;

            public string? IconUrl { get; set; } // اختياري لأيقونة في التطبيق

            public virtual ICollection<Narrative> Narratives { get; set; } = new List<Narrative>();
        }
}




