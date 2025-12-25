using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class HadithCategoryConfiguration : BaseEntityConfiguration<HadithCategory>
    {
        public override void Configure(EntityTypeBuilder<HadithCategory> builder)
        {
            base.Configure(builder);

            builder.ToTable("HadithCategories");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .HasMaxLength(500);
          
            builder.HasMany(c => c.Hadiths)
                .WithOne(h => h.Category)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
