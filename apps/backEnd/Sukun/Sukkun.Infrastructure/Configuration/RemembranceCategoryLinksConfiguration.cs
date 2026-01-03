using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class RemembranceCategoryLinksConfiguration : BaseEntityConfiguration<RemembranceCategoryLinks>
    {
        public override void Configure(EntityTypeBuilder<RemembranceCategoryLinks> builder)
        {
            base.Configure(builder);

            builder.ToTable("RemembranceCategoryLinks");

            builder.HasOne(x => x.Remembrance)
                .WithMany(x => x.RemembranceCategoryLinks)
                .HasForeignKey(x => x.RemembranceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RemembranceCategory)
               .WithMany(x => x.RemembranceCategoryLinks)
               .HasForeignKey(x => x.RemembranceCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
