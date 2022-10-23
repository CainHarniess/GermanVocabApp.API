using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class ListRepositoryQueryTests : ListRepositoryTestConfiguration
{
    private const string zz_deleted = "zz_deleted";
    [Fact]
    public async void GetVocabListInfos_ShouldReturnList_IfNotSoftDeleted()
    {
        Guid activeListId = GetFirstListIdWhere(l => l.DeletedDate.HasValue == false);

        IEnumerable<VocabListInfoDto> listInfos;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            listInfos = await repository.GetVocabListInfos();
        }
        Assert.Contains(listInfos, l => l.Id == activeListId);
    }

    [Fact]
    public async void GetVocabListInfos_ShouldNotReturnList_IfSoftDeleted()
    {
        IEnumerable<VocabListInfoDto> lists;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            lists = await repository.GetVocabListInfos();
        }
        Assert.DoesNotContain(lists, l => l.Name == zz_deleted);
    }

    [Fact]
    public async void GetVocabListInfos_ReturnEmpty_IfNoActiveLists()
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;
        await SoftDeleteAllLists(transactionTimeStamp);

        IEnumerable<VocabListInfoDto> testLists;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            testLists = await repository.GetVocabListInfos();
        }
        Assert.NotNull(testLists);
        Assert.Empty(testLists);
    }

    [Fact]
    public async void Get_ShouldReturnList_IfNotSoftDeleted()
    {
        Guid activeListId = GetFirstListIdWhere(l => l.DeletedDate.HasValue == false);

        VocabListDto? listDto;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            listDto = await repository.Get(activeListId);
        }
        Assert.NotNull(listDto);
    }

    [Fact]
    public async void Get_ShouldNotReturnList_IfSoftDeleted()
    {
        Guid softDeletedListId;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            softDeletedListId = context.Lists.First(l => l.Name == zz_deleted).Id;
        }

        VocabListDto? softDeletedListFromRepo;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            softDeletedListFromRepo = await repository.Get(softDeletedListId);
        }
        Assert.Null(softDeletedListFromRepo);
    }

    [Fact]
    public async void Get_ShouldIncludeListItems_IfNotSoftDeleted()
    {
        VocabListItem activeItemOnActiveList;
        activeItemOnActiveList = await GetFirstItemIdPairWhereAsync(i => i.DeletedDate == null
                                                                      && i.VocabList.DeletedDate == null);

        VocabListDto? testList;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            testList = await repository.Get(activeItemOnActiveList.VocabListId);
        }
        Assert.NotNull(testList);
        Assert.Contains(testList!.ListItems, i => i.Id == activeItemOnActiveList.Id);
    }

    [Fact]
    public async void Get_ShouldNotIncludeListItems_IfSoftDeleted()
    {
        VocabListItem deletedItemOnActiveList;
        deletedItemOnActiveList = await GetFirstItemIdPairWhereAsync(i => i.DeletedDate != null
                                                                      && i.VocabList.DeletedDate == null);

        VocabListDto? testList;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            testList = await repository.Get(deletedItemOnActiveList.VocabListId);
        }
        Assert.NotNull(testList);
        Assert.DoesNotContain(testList!.ListItems, l => l.Id == deletedItemOnActiveList.Id);
    }

    [Fact]
    public async void Get_ShouldReturnNull_IfItemNotFound()
    {
        VocabListDto? testList;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            testList = await repository.Get(Guid.NewGuid());
        }

        Assert.Null(testList);
    }

    private async Task SoftDeleteAllLists(DateTime transactionTimeStamp)
    {
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabList[] lists = await context.Lists
                                             .Where(l => l.DeletedDate == null)
                                             .Select(l => new VocabList
                                             {
                                                 Id = l.Id,
                                                 DeletedDate = l.DeletedDate
                                             })
                                             .ToArrayAsync();
            for (int i = 0; i < lists.Length; i++)
            {
                VocabList list = lists[i];
                context.Attach(list);
                list.DeletedDate = transactionTimeStamp;
            }
            await context.SaveChangesAsync();
        }
    }


    private async Task<VocabListItem> GetFirstItemIdPairWhereAsync(Expression<Func<VocabListItem, bool>> condition)
    {
        using VocabListDbContext context = ContextOptions.BuildNewInMemoryContext();
        return await context.ListItems
                      .Where(condition)
                      .Select(i => new VocabListItem()
                      {
                          Id = i.Id,
                          VocabListId = i.VocabListId,
                      })
                      .FirstAsync();
    }
}