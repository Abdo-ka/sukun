namespace Sukun.Domin.Entities
{
    public class DuaItem : BaseEntity
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;           // عنوان الدعاء
        public string ArabicText { get; set; } = string.Empty;      // النص العربي
        public string? Transliteration { get; set; }                // النطق
        public string? Translation { get; set; }                    // الترجمة
        public string? Reference { get; set; }                      // المصدر
        public int? RepeatCount { get; set; }                       // عدد التكرار
        public string? Virtue { get; set; }                         // الفضل أو المنفعة
        public int DisplayOrder { get; set; } = 1;

        public virtual DuaCategory Category { get; set; } = null!;
    }
}




