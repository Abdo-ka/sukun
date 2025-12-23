using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class DuaCategoryConfiguration : BaseEntityConfiguration<DuaCategory>
    {
        public override void Configure(EntityTypeBuilder<DuaCategory> builder)
        {
            base.Configure(builder);

            builder.ToTable("DuaCategories");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.NameAr)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.NameEn)
                .HasMaxLength(200);

            builder.HasMany(c => c.Duas)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
