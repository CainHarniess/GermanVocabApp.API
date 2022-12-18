using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GermanVocabApp.DataAccess.EntityFramework.Vocab.Models.Configuration;

internal class VocabListEntityConfiguration : IEntityTypeConfiguration<VocabList>
{
    public void Configure(EntityTypeBuilder<VocabList> builder)
    {
        builder.HasMany(vl => vl.ListItems)
               .WithOne(li => li.VocabList)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Property(vl => vl.Name)
               .HasMaxLength(100);

        builder.Property(vl => vl.Description)
               .HasMaxLength(250);
    }
}
