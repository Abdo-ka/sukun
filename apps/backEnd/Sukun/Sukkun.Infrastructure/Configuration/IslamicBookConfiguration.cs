using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class IslamicBookConfiguration : BaseEntityConfiguration<IslamicBook>
    {
        public override void Configure(EntityTypeBuilder<IslamicBook> builder)
        {
            base.Configure(builder);

            builder.ToTable("IslamicBooks");

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(b => b.NameAr)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(b => b.Type)
                .HasConversion<int>()
                .IsRequired();

        }
    }
}
