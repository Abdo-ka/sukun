namespace Sukun.Domin.Entities
{
    public class DuaCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;           // مثل: "أدعية السفر"
        public string NameAr { get; set; } = string.Empty;
        public string? NameEn { get; set; }
        public virtual ICollection<DuaItem> Duas { get; set; } = new List<DuaItem>();
    }
}




