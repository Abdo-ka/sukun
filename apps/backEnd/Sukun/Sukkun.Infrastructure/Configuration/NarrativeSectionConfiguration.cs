using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class NarrativeSectionConfiguration : BaseEntityConfiguration<NarrativeSection>
    {
        public override void Configure(EntityTypeBuilder<NarrativeSection> builder)
        {
            base.Configure(builder);

            builder.ToTable("NarrativeSections");

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Content)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(s => s.MediaUrl)
                .HasMaxLength(500);

            builder.Property(s => s.DisplayOrder)
                .IsRequired();

            builder.HasIndex(s => new { s.NarrativeId, s.DisplayOrder });
        }
    }
}
