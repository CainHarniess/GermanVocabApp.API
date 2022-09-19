using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.ModificationExtensions;

internal static class VocabListDbSetQueryExtensions
{
    public static async Task<VocabList> TryGetFirstActiveWithId(this DbSet<VocabList> dbSet,
                                                                Guid id)
    {
        try
        {
            return await dbSet.ActiveWithListItems(id)
                              .FirstAsync(l => l.Id == id);
        }
        catch (InvalidOperationException e)
        {
            throw new EntityNotFoundException($"No active {nameof(VocabList)} entity satisfying the provided predicate.", e);
        }
    }

    public static IQueryable<VocabList> ActiveWithListItems(this DbSet<VocabList> dbSet,
                                                                  Guid id)
    {
        return dbSet.Where(l => l.DeletedDate == null)
                    .Include(vl => vl.ListItems
                                     .Where(li => li.DeletedDate == null));
    }

    public static async void FirstWithListItemsWhereAsync(this DbSet<VocabList> dbSet,
        Predicate<VocabList> listFilter, Predicate<VocabListItem> listItemFilter)
    {
        VocabList entity = await dbSet.Where(e => listFilter(e))
                                           .Include(vl => vl.ListItems
                                           .Where(li => listItemFilter(li)))
                                           .FirstAsync();
    }
}
