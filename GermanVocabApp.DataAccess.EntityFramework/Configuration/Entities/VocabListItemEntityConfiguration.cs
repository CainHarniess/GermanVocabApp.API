using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GermanVocabApp.DataAccess.EntityFramework.Configuration.Entities;

internal class VocabListItemEntityConfiguration : IEntityTypeConfiguration<VocabListItem>
{
    public void Configure(EntityTypeBuilder<VocabListItem> builder)
    {
        ConfigureConversions(builder);
        ConfigureCharacterLengths(builder);
    }
    
    private static void ConfigureConversions(EntityTypeBuilder<VocabListItem> builder)
    {
        builder.Property(n => n.WordType)
                       .HasConversion<string>();

        builder.Property(n => n.ReflexiveCase)
               .HasConversion<string>();

        builder.Property(n => n.Separability)
               .HasConversion<string>();

        builder.Property(n => n.Transitivity)
               .HasConversion<string>();

        builder.Property(n => n.AuxiliaryVerb)
               .HasConversion<string>();

        builder.Property(n => n.Gender)
               .HasConversion<string>();

        builder.Property(n => n.PrepositionCase)
               .HasConversion<string>();

        builder.Property(n => n.FixedPlurality)
               .HasConversion<string>();
    }
   
    private static void ConfigureCharacterLengths(EntityTypeBuilder<VocabListItem> builder)
    {
        builder.Property(n => n.WordType)
                       .HasMaxLength(10);

        builder.Property(n => n.Separability)
               .HasMaxLength(15);

        builder.Property(n => n.Transitivity)
               .HasMaxLength(15);

        builder.Property(n => n.ReflexiveCase)
               .HasMaxLength(10);

        builder.Property(n => n.ThirdPersonPresent)
               .HasMaxLength(25);

        builder.Property(n => n.ThirdPersonImperfect)
               .HasMaxLength(25);

        // TODO: Fix the below max length constraint.
        builder.Property(n => n.ThirdPersonImperfect)
               .HasMaxLength(6);

        builder.Property(n => n.Perfect)
               .HasMaxLength(25);

        builder.Property(n => n.Gender)
               .HasMaxLength(10);

        builder.Property(n => n.German)
               .HasMaxLength(100);

        builder.Property(n => n.Plural)
               .HasMaxLength(100);

        builder.Property(n => n.Preposition)
               .HasMaxLength(25);

        builder.Property(n => n.PrepositionCase)
               .HasMaxLength(10);

        builder.Property(n => n.Comparative)
               .HasMaxLength(100);

        builder.Property(n => n.Superlative)
               .HasMaxLength(100);

        builder.Property(n => n.English)
               .HasMaxLength(100);

        builder.Property(n => n.FixedPlurality)
               .HasMaxLength(10);
    }

}
