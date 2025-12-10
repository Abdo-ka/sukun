using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Infrastructure.Configuration;
using System.Reflection;

namespace Sukun.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<FCMToken> FCMTokens { get; set; }
        public DbSet<QuranSurah> QuranSurahs { get; set; }
        public DbSet<QuranVerse> QuranVerses { get; set; }
        public DbSet<Tafsir> Tafsirs { get; set; }
        public DbSet<UserBookmark> UserBookmarks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Global Query Filter for soft delete 
            modelBuilder.Entity<BaseEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

    }

}
