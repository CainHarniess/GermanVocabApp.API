using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.Shared.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GermanVocabApp.DataAccess.EntityFramework.Configuration.Entities;

internal class VocabListEntityConfiguration : IEntityTypeConfiguration<VocabList>
{
    public void Configure(EntityTypeBuilder<VocabList> builder)
    {
        builder.HasMany(vl => vl.ListItems)
               .WithOne(li => li.VocabList)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Property(vl => vl.Name)
               .HasMaxLength(VocabListValidationData.NameMaxLength);

        builder.Property(vl => vl.Description)
               .HasMaxLength(VocabListValidationData.DescriptionMaxLength);
    }
}
