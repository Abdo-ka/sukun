using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukun.Domin.Entities;
using System.Reflection.Emit;

namespace Sukun.Infrastructure.Configuration
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties Configuration
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.CreateAt)
                .IsRequired()
                .HasColumnType("datetime2")
                 .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property("Version")
                   .HasColumnName("Version")
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
