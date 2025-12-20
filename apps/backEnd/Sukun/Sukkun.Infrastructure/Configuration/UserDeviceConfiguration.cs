using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class UserDeviceConfiguration : BaseEntityConfiguration<UserDevice>
    {
        public override void Configure(EntityTypeBuilder<UserDevice> builder)
        {
            base.Configure(builder);

            // Table name
            builder.ToTable("UserDevices");

            builder.Property(d => d.DeviceId)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(d => d.DeviceType)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasConversion<string>();

            builder.Property(d => d.DeviceModel)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(d => d.IsNotificationsEnabled)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(d => d.LastNotificationSent)
                .HasColumnType("datetime2");

            builder.Property(d => d.LastActiveDate)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(d => d.AppVersion)
                .HasMaxLength(20)
                .IsRequired(false);


            // Navigation Properties Configuration

            builder.HasMany(d => d.FCMTokens)
                .WithOne(t => t.Device)
                .HasForeignKey(t => t.UserDeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
