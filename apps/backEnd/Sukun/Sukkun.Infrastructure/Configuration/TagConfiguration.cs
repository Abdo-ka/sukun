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

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.NameAr)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(t => t.Name)
            .IsUnique();

        }
    }
}
