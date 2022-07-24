using GermanVocabApp.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Configuration;

internal static class VocabListConfigurationExtensions
{
    public static void Configure(this ModelBuilder modelBuilder)
    {
        modelBuilder.RemoveVocabListItemDbCascadeDelete();
    }

    private static void RemoveVocabListItemDbCascadeDelete(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VocabList>()
            .HasMany(vl => vl.ListItems)
            .WithOne(li => li.VocabList)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
