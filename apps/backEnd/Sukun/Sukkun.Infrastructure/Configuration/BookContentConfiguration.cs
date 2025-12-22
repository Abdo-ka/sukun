using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class BookContentConfiguration : BaseEntityConfiguration<BookContent>
    {
        public override void Configure(EntityTypeBuilder<BookContent> builder)
        {
            base.Configure(builder);

            builder.ToTable("BookContents");

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(int.MaxValue); // نص طويل

            builder.Property(c => c.MediaUrl)
                .HasMaxLength(500);

            builder.HasOne(c => c.Section)
                .WithMany(s => s.Contents)
                .HasForeignKey(c => c.SectionId)
                .OnDelete(DeleteBehavior.Cascade); // حذف المحتوى مع الباب

            builder.HasIndex(c => c.SectionId);
            builder.HasIndex(c => c.DisplayOrder);
        }
    }
}
