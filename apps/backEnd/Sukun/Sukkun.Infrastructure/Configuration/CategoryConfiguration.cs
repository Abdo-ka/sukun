using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.ToTable("Categories");

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.TitleAr)
                .HasMaxLength(200);

            builder.Property(c => c.IsMainSection)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.Order)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.ParentId);
            builder.HasIndex(c => c.IsMainSection);
            builder.HasIndex(c => c.Order);
        }
    }
}
