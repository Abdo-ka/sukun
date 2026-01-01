using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sukun.Domin.Entities;

namespace Sukun.Infrastructure.Configuration
{
    public class NarrativeTagsConfiguration : BaseEntityConfiguration<NarrativeTags>
    {
        public override void Configure(EntityTypeBuilder<NarrativeTags> builder)
        {
            base.Configure(builder);

            builder.ToTable("NarrativeTags");

           builder.HasOne(x=>x.Tag)
                .WithMany(x=>x.NarrativeTags)
                .HasForeignKey(x=>x.TagId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(x => x.Narrative)
               .WithMany(x => x.NarrativeTags)
               .HasForeignKey(x => x.NarrativeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
