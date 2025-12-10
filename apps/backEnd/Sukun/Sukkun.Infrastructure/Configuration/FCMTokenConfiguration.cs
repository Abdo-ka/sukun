using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class FCMTokenConfiguration : BaseEntityConfiguration<FCMToken>
    {
        public override void Configure(EntityTypeBuilder<FCMToken> builder)
        {
            base.Configure(builder);

            builder.ToTable("FCMTokens");

            builder.Property(t => t.Token)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.LastUsedAt)
                .HasColumnType("datetime2");

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

        }
    }
}
