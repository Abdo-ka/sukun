namespace Sukun.Domin.Entities
{
    public class User : BaseEntity
    {
        public Guid? CityId { get; set; }
        // Navigation properties
        public virtual City City { get; set; }
        public virtual ICollection<UserDevice> Devices { get; set; } = new List<UserDevice>();
        public virtual ICollection<UserBookmark> Bookmarks { get; set; } = new List<UserBookmark>();
    }
}




