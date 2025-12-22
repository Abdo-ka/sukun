namespace Sukun.Domin.Entities
{
    public class HadithExplanation : BaseEntity
    {
        public Guid HadithId { get; set; }
        public string Scholar { get; set; }
        public string Explanation { get; set; }
        //public string? Language { get; set; }

        public virtual Hadith Hadith { get; set; }
    }

}




