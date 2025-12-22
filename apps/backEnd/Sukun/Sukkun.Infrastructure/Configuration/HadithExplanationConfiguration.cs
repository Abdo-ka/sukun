using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class HadithExplanationConfiguration : BaseEntityConfiguration<HadithExplanation>
    {
        public override void Configure(EntityTypeBuilder<HadithExplanation> builder)
        {
            base.Configure(builder);

            builder.ToTable("HadithExplanations");

            builder.Property(e => e.Scholar)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Explanation)
                .IsRequired()
                .HasMaxLength(10000); // شرح طويل

            builder.HasOne(e => e.Hadith)
                .WithMany(h => h.Explanations)
                .HasForeignKey(e => e.HadithId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
