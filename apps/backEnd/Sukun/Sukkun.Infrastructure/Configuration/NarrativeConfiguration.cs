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

            builder.Property(n => n.CoverImageUrl)
                .HasMaxLength(500);

            builder.Property(n => n.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(n => n.Parent)
                .WithMany(n => n.Children)
                .HasForeignKey(n => n.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(n => n.Sections)
                .WithOne(s => s.Narrative)
                .HasForeignKey(s => s.NarrativeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
      
            builder.HasMany(n => n.Tags)
                   .WithMany(t => t.Narratives)
                   .UsingEntity(j => j.ToTable("NarrativeTags"));

            builder.HasIndex(n => n.Type);
            builder.HasIndex(n => n.IsFeatured);
        }
    }
}
