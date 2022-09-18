using GermanVocabApp.DataAccess.Shared.Abstractions;
using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.ModificationExtension;

internal static class SoftDeletableExtensions
{
    public static void SoftDeleteAll(this IEnumerable<ISoftDeletable> entities, DateTime deletionTimestamp)
    {
        entities.ForEach(softDeletable =>
        {
            softDeletable.DeletedDate = deletionTimestamp;
        });
    }

    public static void SoftDeleteWhere(this IEnumerable<ISoftDeletable> entities, Predicate<ISoftDeletable> pred, DateTime deletionTimestamp)
    {
        entities.ForEach(softDeletable =>
        {
            if (!pred(softDeletable))
            {
                return;
            }
            softDeletable.DeletedDate = deletionTimestamp;
        });
    }
}
