namespace Sukun.Domin.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? TitleAr { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsMainSection { get; set; } = false;
        public int Order { get; set; } = 0;

        // علاقات
        public Category? Parent { get; set; }
        public ICollection<Category> Children { get; set; } = new List<Category>();
        public ICollection<NarrativeCategory> NarrativeCategories { get; set; } = new List<NarrativeCategory>();
    }

}




