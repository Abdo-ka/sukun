using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class QuranVerseConfiguration : BaseEntityConfiguration<QuranVerse>
    {
        public override void Configure(EntityTypeBuilder<QuranVerse> builder)
        {
            base.Configure(builder);

            builder.ToTable("QuranVerses");

           builder.Property(s => s.JuzNumber)
                .IsRequired();

            builder.HasIndex(s => s.PageNumber)
                .IsUnique();

            builder.Property(s => s.Text)
                .IsRequired()
                .HasColumnType("nvarchar(MAX)")
                .HasMaxLength(500);

            builder.Property(s => s.VerseNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.HizbNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(v=>v.Tafsirs)
                   .WithOne(v=>v.Verse)
                   .HasForeignKey(t=>t.VerseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.Bookmarks)
              .WithOne(b => b.Verse)
              .HasForeignKey(b => b.VerseId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
