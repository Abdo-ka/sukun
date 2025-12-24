using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class RemembranceConfiguration : BaseEntityConfiguration<Remembrance>
    {
        public override void Configure(EntityTypeBuilder<Remembrance> builder)
        {
            base.Configure(builder);

            builder.ToTable("Remembrances");

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(r => r.Text)
                .IsRequired();

            builder.Property(r => r.Benefits)
                .HasMaxLength(2000);


            builder.HasMany(r => r.Contents)
                .WithOne(c => c.Remembrance)
                .HasForeignKey(c => c.RemembranceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
