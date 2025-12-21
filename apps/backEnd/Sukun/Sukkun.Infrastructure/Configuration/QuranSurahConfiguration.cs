using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

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

            //builder.Property(s => s.EnglishNameTranslation)
            //    .IsRequired()
            //    .HasMaxLength(100);

            builder.Property(s => s.RevelationType)
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(RevelationType.Makki)
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
