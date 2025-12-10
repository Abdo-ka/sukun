using Sukun.Domin.Enums;

namespace Sukun.Domin.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public Guid? CityId { get; set; }

        // Navigation properties
        public virtual City City { get; set; }
        public virtual ICollection<UserDevice> Devices { get; set; } = new List<UserDevice>();
        public virtual ICollection<UserBookmark> Bookmarks { get; set; } = new List<UserBookmark>();
    }

    public class City:BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; } // Arabic name
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int TimeZone { get; set; }
        // Navigation Properties
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

    public class UserDevice : BaseEntity
    {
        public Guid UserId { get; set; }
        // Device Info
        public string DeviceId { get; set; }
        public string DeviceType { get; set; } // iOS, Android, Web
        public string DeviceModel { get; set; }

        // Push Notifications
        public bool IsNotificationsEnabled { get; set; }
        public DateTime? LastNotificationSent { get; set; }

        // Tracking
        public DateTime LastActiveDate { get; set; }
        public string? AppVersion { get; set; }

        // Navigation Property
        public virtual User User { get; set; }
        public ICollection<FCMToken> FCMTokens { get; set; }= new List<FCMToken>();
    }
    public class FCMToken : BaseEntity
    {
        public Guid UserDeviceId { get; set; }
        public string Token { get; set; }

        public DateTime? LastUsedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual UserDevice Device { get; set; }
    }

    public class QuranSurah : BaseEntity
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public int RevelationOrder { get; set; }
        public RevelationType RevelationType { get; set; } // Makki, Madani
        public int TotalVerses { get; set; }

        // Navigation Properties
        public virtual ICollection<QuranVerse> Verses { get; set; } = new List<QuranVerse>();
    }
    public class QuranVerse : BaseEntity
    {
        public Guid SurahId { get; set; }
        public int VerseNumber { get; set; }
        public int PageNumber { get; set; }
        public int JuzNumber { get; set; }
        public int HizbNumber { get; set; }
        public string Text { get; set; }

        // Navigation Properties
        public virtual QuranSurah Surah { get; set; }
        public virtual ICollection<Tafsir> Tafsirs { get; set; } = new List<Tafsir>();
        public virtual ICollection<UserBookmark> Bookmarks { get; set; } = new List<UserBookmark>();
    }
    public class Tafsir : BaseEntity
    {
        public Guid? VerseId { get; set; }

        // Tafsir Info
        public TafsirSource Source { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        // Navigation Properties
        public virtual QuranVerse? Verse { get; set; }
    }
    public class UserBookmark : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid VerseId { get; set; }

        // Bookmark Info
        public string? Note { get; set; }
        public BookmarkType Type { get; set; } // Favorite, Memorize, Understand

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual QuranVerse Verse { get; set; }
    }
}




