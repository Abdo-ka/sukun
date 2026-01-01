using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class NarrativeConfiguration : BaseEntityConfiguration<Narrative>
    {
        public override void Configure(EntityTypeBuilder<Narrative> builder)
        {
            base.Configure(builder);

            builder.ToTable("Narratives");

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.TitleAr)
                .HasMaxLength(200);

            builder.Property(n => n.ShortDescription)
                .HasMaxLength(500);

            builder.Property(n => n.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(n => n.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);

            builder.Property(n => n.ViewCount)
            .IsRequired()
            .HasDefaultValue(0);

            builder.HasIndex(n => n.Type);
            builder.HasIndex(n => n.IsFeatured);
        }
    }
}
