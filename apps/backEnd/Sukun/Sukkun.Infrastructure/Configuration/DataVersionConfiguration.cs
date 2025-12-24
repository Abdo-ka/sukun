using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class DataVersionConfiguration : BaseEntityConfiguration<DataVersion>
    {
        public override void Configure(EntityTypeBuilder<DataVersion> builder)
        {
            base.Configure(builder);

            builder.ToTable("DataVersions");

            builder.HasIndex(d => d.TableName)
                .IsUnique(); // كل جدول له سجل واحد فقط

            builder.Property(d => d.TableName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Version)
                .IsRequired();
        }
    }

}
