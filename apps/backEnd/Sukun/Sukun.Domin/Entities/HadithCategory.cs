namespace Sukun.Domin.Entities
{
    public class HadithCategory : BaseEntity
    {
        public string Name { get; set; }
        //public string NameAr { get; set; }
        public string Description { get; set; }
        // Navigation Properties
        public virtual ICollection<Hadith> Hadiths { get; set; } = new List<Hadith>();
    }

}




