using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Infrastructure.Configuration
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.FullName)
             .HasMaxLength(200)
             .IsRequired(false);

            builder.Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<int>() // Store enum as int
                .HasDefaultValue(UserRole.User);

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.LastLoginDate)
                .HasColumnType("datetime2");

            builder.Property(u => u.ProfileImageUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasIndex(u => u.IsActive);

            // Navigation Properties Configuration
            builder.HasMany(u => u.Devices)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Bookmarks)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
