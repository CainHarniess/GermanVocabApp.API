using GermanVocabApp.DataAccess.EntityFramework.Configuration.Entities;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Configuration;

internal static class ModelBuilderEntityConfigurationExtensions
{
    public static void ConfigureEntities(this ModelBuilder modelBuilder)
    {
        new VocabListEntityConfiguration().Configure(modelBuilder.Entity<VocabList>());
        new VocabListItemEntityConfiguration().Configure(modelBuilder.Entity<VocabListItem>());
    }
}
