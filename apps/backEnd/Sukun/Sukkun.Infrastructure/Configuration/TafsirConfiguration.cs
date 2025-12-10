using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Infrastructure.Configuration
{
    public class TafsirConfiguration : BaseEntityConfiguration<Tafsir>
    {
        public override void Configure(EntityTypeBuilder<Tafsir> builder)
        {
            base.Configure(builder);

            builder.ToTable("Tafsirs");

            builder.Property(t => t.Source)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(TafsirSource.IbnKathir);

            builder.Property(t => t.Author)
                .HasMaxLength(200)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(t => t.Text)
                .IsRequired()
                .HasColumnType("nvarchar(MAX)");
        }
    }
}
