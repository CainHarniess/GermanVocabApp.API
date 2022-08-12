using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GermanVocabApp.DataAccess.EntityFramework.Configuration;

internal static class ModelBuilderDatabaseConfigurationExtensions
{
    public static void ConfigureDatabaseConventions(this ModelBuilder modelBuilder)
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
