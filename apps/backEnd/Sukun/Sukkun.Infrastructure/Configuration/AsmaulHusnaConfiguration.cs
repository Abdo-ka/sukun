using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class AsmaulHusnaConfiguration : BaseEntityConfiguration<AsmaulHusna>
    {
        public override void Configure(EntityTypeBuilder<AsmaulHusna> builder)
        {
            base.Configure(builder);

            builder.ToTable("AsmaulHusna");

            builder.Property(a => a.Number)
                .IsRequired();

            builder.Property(a => a.NameArabic)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.NameTransliteration)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.MeaningArabic)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(a => a.MeaningEnglish)
                .HasMaxLength(300);

            builder.HasIndex(a => a.Number)
                .IsUnique();
        }
    }
}
