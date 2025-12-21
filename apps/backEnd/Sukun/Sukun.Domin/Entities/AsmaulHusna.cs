namespace Sukun.Domin.Entities
{
    public class AsmaulHusna : BaseEntity
    {
        public int Number { get; set; } 
        public string NameArabic { get; set; } = string.Empty; 
        public string NameTransliteration { get; set; } = string.Empty; 
        public string MeaningArabic { get; set; } = string.Empty; 
        public string MeaningEnglish { get; set; } = string.Empty; 
    }
}




