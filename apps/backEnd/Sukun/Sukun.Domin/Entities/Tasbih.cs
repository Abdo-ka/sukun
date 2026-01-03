using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class Tasbih : BaseEntity
    {
        public string Title { get; set; } = string.Empty;           // مثل: "سبحان الله", "الرحمن", "الله أكبر"
        public string TitleEn { get; set; } = string.Empty;         
        public string? Benefits { get; set; }                       // الفضل أو المنفعة
        public int RecommendedCount { get; set; } = 100;             // عدد التسبيح الموصى (مثل 33, 100)
        public string? Reference { get; set; }                      // المصدر (حديث أو قرآن)
        public int Order { get; set; } = 0;                         // للترتيب في القائمة

    }
  
}




