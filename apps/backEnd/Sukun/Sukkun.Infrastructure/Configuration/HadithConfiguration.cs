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
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(h => h.Text)
                .IsRequired()
                .HasMaxLength(5000); // حد مناسب للحديث

            builder.Property(h => h.Grade)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(h => h.GradedBy)
                .HasMaxLength(200);

            builder.Property(h => h.GradeExplanation)
                .HasMaxLength(2000);

            builder.Property(h => h.BookName)
                .HasMaxLength(200);

            builder.Property(h => h.ChapterName)
                .HasMaxLength(300);

            builder.HasIndex(h => h.Reference);
            builder.HasIndex(h => h.Grade);
            builder.HasIndex(h => h.CategoryId);

            builder.HasOne(h => h.Category)
                .WithMany(c => c.Hadiths)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // تجنب cascade مع soft delete

            builder.HasMany(h => h.Explanations)
                .WithOne(e => e.Hadith)
                .HasForeignKey(e => e.HadithId)
                .OnDelete(DeleteBehavior.ClientSetNull); // نفس حل Narrative

            builder.HasOne(h => h.Book)
                .WithMany(b => b.Hadiths)
                .HasForeignKey(h => h.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Section)
                .WithMany(s => s.Hadiths)
                .HasForeignKey(h => h.SectionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
