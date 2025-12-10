using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;
using Sukun.Domin.Enums;

namespace Sukun.Infrastructure.Configuration
{
    public class UserBookmarkConfiguration : BaseEntityConfiguration<UserBookmark>
    {
        public override void Configure(EntityTypeBuilder<UserBookmark> builder)
        {
            base.Configure(builder);

            builder.ToTable("UserBookmarks");

            builder.Property(b => b.Note)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(b => b.Type)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(BookmarkType.Favorite);
        }
    }
}
