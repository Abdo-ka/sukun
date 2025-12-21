using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class TagConfiguration : BaseEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);

            builder.ToTable("Tags");

            builder.Property(t => t.TagType)
                .IsRequired();

            builder.Property(t => t.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.NameEn)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.IconUrl)
                .HasMaxLength(500);

            // كل TagType واحد فقط في الجدول (لا تكرار)
            builder.HasIndex(t => t.TagType)
                .IsUnique();
        }
    }
}
