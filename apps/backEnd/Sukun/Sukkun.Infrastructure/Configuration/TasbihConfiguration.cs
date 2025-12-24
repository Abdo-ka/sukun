using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class TasbihConfiguration : BaseEntityConfiguration<Tasbih>
    {
        public override void Configure(EntityTypeBuilder<Tasbih> builder)
        {
            base.Configure(builder);

            builder.ToTable("Tasbihs");

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.TitleAr)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Benefits)
                .HasMaxLength(1000);

            builder.Property(t => t.Reference)
                .HasMaxLength(500);
        }
    }
}
