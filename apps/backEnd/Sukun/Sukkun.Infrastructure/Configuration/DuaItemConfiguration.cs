using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class DuaItemConfiguration : BaseEntityConfiguration<DuaItem>
    {
        public override void Configure(EntityTypeBuilder<DuaItem> builder)
        {
            base.Configure(builder);

            builder.ToTable("DuaItems");

            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(d => d.Text)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(d => d.TextEn)
                .HasMaxLength(1000)
                .IsRequired(false);


            builder.Property(d => d.Reference)
                .HasMaxLength(300);

            builder.Property(d => d.Virtue)
                .HasMaxLength(1000);

            builder.HasIndex(d => d.CategoryId);
            builder.HasIndex(d => d.DisplayOrder);
        }
    }
}
