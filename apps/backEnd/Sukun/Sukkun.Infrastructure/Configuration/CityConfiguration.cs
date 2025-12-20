using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class CityConfiguration : BaseEntityConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);

            builder.ToTable("Cities");

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.NameAr)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Country)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.CountryCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Latitude)
                .HasColumnType("decimal(9,6)")
                .IsRequired();

            builder.Property(c => c.Longitude)
                .HasColumnType("decimal(9,6)")
                .IsRequired();

            builder.Property(c => c.TimeZone)
                .IsRequired();


        }
    }
}
