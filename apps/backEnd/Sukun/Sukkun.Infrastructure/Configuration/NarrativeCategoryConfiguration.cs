using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class NarrativeCategoryConfiguration : BaseEntityConfiguration<NarrativeCategory>
    {
        public override void Configure(EntityTypeBuilder<NarrativeCategory> builder)
        {
            base.Configure(builder);

            builder.ToTable("NarrativeCategories");


            builder.Property(nc => nc.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(nc => nc.Narrative)
                .WithMany(n => n.NarrativeCategories)
                .HasForeignKey(nc => nc.NarrativeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(nc => nc.Category)
                .WithMany(c => c.NarrativeCategories)
                .HasForeignKey(nc => nc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // فهرس لتحسين جلب المحتويات حسب القسم
            builder.HasIndex(nc => nc.CategoryId);
        }
    }
}
