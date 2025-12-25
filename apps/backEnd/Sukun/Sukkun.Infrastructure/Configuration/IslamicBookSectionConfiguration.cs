using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class IslamicBookSectionConfiguration : BaseEntityConfiguration<IslamicBookSection>
    {
        public override void Configure(EntityTypeBuilder<IslamicBookSection> builder)
        {
            base.Configure(builder);

            builder.ToTable("IslamicBookSections");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(s => s.ChapterNumber)
                   .IsRequired(false);

          
            builder.HasOne(s => s.Book)
                .WithMany(b => b.Sections)
                .HasForeignKey(s => s.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Contents)
                   .WithOne(c => c.Section)
                   .HasForeignKey(c => c.SectionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
