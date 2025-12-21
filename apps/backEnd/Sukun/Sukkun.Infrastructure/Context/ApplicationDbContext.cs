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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tafsir> Tafsirs { get; set; }
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
