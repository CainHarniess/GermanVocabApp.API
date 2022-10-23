using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class ListRepositoryHardDeleteCommandTests : ListRepositoryTestConfiguration
{
    [Fact]
    public async void HardDelete_ShouldReturnFalse_IfListWithIdNotFound()
    {
        bool result;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            result = await repository.HardDelete(Guid.NewGuid());
        }
        Assert.False(result);
    }

    [Fact]
    public async void HardDelete_ShouldReturnFalse_IfListAlreadyDeleted()
    {
        Guid deletedListId = GetFirstListIdWhere(l => l.DeletedDate.HasValue);

        bool result;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            result = await repository.HardDelete(deletedListId);
        }
        Assert.False(result);
    }

    [Fact]
    public async void HardDelete_ShouldReturnTrue_IfListFound()
    {
        Guid activeListId = GetFirstListIdWhere(l => l.DeletedDate.HasValue == false);

        bool result;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            result = await repository.HardDelete(activeListId);
        }
        Assert.True(result);
    }


    [Fact]
    public async void HardDelete_ShouldRemoveList_IfNotDeleted()
    {
        Guid listId = GetFirstListIdWhere(l => l.DeletedDate.HasValue == false);

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            bool _ = await repository.HardDelete(listId);
        }

        VocabList? testList;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            testList = context.Lists
                              .Where(l => l.Id == listId)
                              .FirstOrDefault();
        };
        Assert.Null(testList);
    }

    [Fact]
    public async void HardDelete_ShouldRemoveItems()
    {
        Guid listId = GetFirstListIdWhere(l => l.ListItems.Count() > 1);

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            bool _ = await repository.HardDelete(listId);
        }

        Guid[] itemIds;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            itemIds = context.ListItems
                             .Where(i => i.VocabListId == listId)
                             .Select(i => i.Id)
                             .ToArray();
        }
        Assert.Empty(itemIds);
    }

}
