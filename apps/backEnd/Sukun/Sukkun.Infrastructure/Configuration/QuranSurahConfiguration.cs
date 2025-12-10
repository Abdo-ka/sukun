using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class QuranSurahConfiguration : BaseEntityConfiguration<QuranSurah>
    {
        public override void Configure(EntityTypeBuilder<QuranSurah> builder)
        {
            base.Configure(builder);

            builder.ToTable("QuranSurahs");

           builder.Property(s => s.Number)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.EnglishName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.RevelationType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(s => s.RevelationOrder)
                .IsRequired();


            // Relationships
            builder.HasMany(s => s.Verses)
                .WithOne(v => v.Surah)
                .HasForeignKey(v => v.SurahId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
