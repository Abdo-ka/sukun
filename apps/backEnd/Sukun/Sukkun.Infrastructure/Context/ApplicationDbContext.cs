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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<QuranSurah> QuranSurahs { get; set; }
        public DbSet<QuranVerse> QuranVerses { get; set; }
        public DbSet<AsmaulHusna> AsmaulHusna { get; set; }
        public DbSet<Narrative> Narratives { get; set; }
        public DbSet<NarrativeSection> NarrativeSections { get; set; }
        public DbSet<NarrativeCategory> NarrativeCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NarrativeTags> NarrativeTags { get; set; }
        public DbSet<Tafsir> Tafsirs { get; set; }
        public DbSet<IslamicBook> IslamicBooks { get; set; }
        public DbSet<IslamicBookSection> IslamicBookSections { get; set; }
        public DbSet<BookContent> BookContents { get; set; }
        public DbSet<DuaCategory> DuaCategories { get; set; }
        public DbSet<DuaItem> DuaItems { get; set; }
        public DbSet<Remembrance> Remembrances { get; set; }
        public DbSet<RemembranceCategory> RemembranceCategories { get; set; }
        public DbSet<RemembranceCategoryLinks> RemembranceCategoryLinks { get; set; }
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
