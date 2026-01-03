using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class RemembranceCategoryConfiguration : BaseEntityConfiguration<RemembranceCategory>
    {
        public override void Configure(EntityTypeBuilder<RemembranceCategory> builder)
        {
            base.Configure(builder);

            builder.ToTable("RemembranceCategories");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.NameEn)
                .HasMaxLength(200);
        }
    }
}
