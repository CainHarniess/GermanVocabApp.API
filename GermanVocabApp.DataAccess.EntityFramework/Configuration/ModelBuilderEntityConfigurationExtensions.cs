using GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;
using GermanVocabApp.DataAccess.EntityFramework.Authentication.Models.Configuration;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Configuration;

internal static class ModelBuilderEntityConfigurationExtensions
{
    public static void ConfigureEntities(this ModelBuilder modelBuilder)
    {
        new VocabListEntityConfiguration().Configure(modelBuilder.Entity<VocabList>());
        new VocabListItemEntityConfiguration().Configure(modelBuilder.Entity<VocabListItem>());
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
    }
}
