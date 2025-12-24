using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class MosqueConfiguration : BaseEntityConfiguration<Mosque>
    {
        public override void Configure(EntityTypeBuilder<Mosque> builder)
        {
            base.Configure(builder);

            builder.ToTable("Mosques");

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.NameAr)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(m => m.Website)
                .HasMaxLength(300);

            builder.Property(m => m.Email)
                .HasMaxLength(200);
        }
    }
}
