using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class HadithConfiguration : BaseEntityConfiguration<Hadith>
    {
        public override void Configure(EntityTypeBuilder<Hadith> builder)
        {
            base.Configure(builder);

            builder.ToTable("Hadiths");

            builder.Property(h => h.Reference)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(h => h.Text)
                .IsRequired()
                .HasMaxLength(5000); 

            builder.Property(h => h.Grade)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(h => h.Category)
                .WithMany(c => c.Hadiths)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false); 

            builder.HasMany(h => h.Explanations)
                .WithOne(e => e.Hadith)
                .HasForeignKey(e => e.HadithId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false); 

            builder.HasOne(h => h.Book)
                .WithMany(b => b.Hadiths)
                .HasForeignKey(h => h.BookId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false); ;

            builder.HasOne(h => h.Section)
                .WithMany(s => s.Hadiths)
                .HasForeignKey(h => h.SectionId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false); ;
        }
    }
}
