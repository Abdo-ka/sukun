using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Infrastructure.Configuration
{
    public class AdminConfiguration : BaseEntityConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(a => a.Email)
                .IsUnique()
                .HasDatabaseName("IX_Admins_Email");

            builder.Property(a => a.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(a => a.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(a => a.Role)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(AdminRole.Admin);

            builder.Property(a => a.LastLoginDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(a => a.LastPasswordChange)
                .IsRequired(false)
                .HasColumnType("datetime2");

        }
    }
}
