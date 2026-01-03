using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class RemembranceContentConfiguration : BaseEntityConfiguration<RemembranceContent>
    {
        public override void Configure(EntityTypeBuilder<RemembranceContent> builder)
        {
            base.Configure(builder);

            builder.ToTable("RemembranceContents");

            builder.Property(c => c.SourceType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(c => c.CustomContent)
                .HasMaxLength(int.MaxValue)
                .IsRequired(false);

            builder.Property(c => c.SourceId)
             .IsRequired(false);

        }
    }
}
