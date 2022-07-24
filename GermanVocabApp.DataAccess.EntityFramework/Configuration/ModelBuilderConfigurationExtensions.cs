using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GermanVocabApp.DataAccess.EntityFramework.Configuration;

internal static class ModelBuilderConfigurationExtensions
{
    public static void ConfigureConventions(this ModelBuilder modelBuilder)
    {
        modelBuilder.RemoveTablePluralisationConvention();
    }

    private static void RemoveTablePluralisationConvention(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();
        foreach (IMutableEntityType entityType in entityTypes)
        {
            entityType.SetTableName(entityType.DisplayName());
        }
    }
}
