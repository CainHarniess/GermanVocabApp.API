using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Linq.Expressions;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class ListRepositoryCommandTests : ListRepositoryTestConfiguration
{
    [Fact]
    public async void Add_ShouldAddListAndListItems()
    {
        VocabListItemDto[] newItems = new VocabListItemDto[]
        {
            ItemDtoBuilder.Spicy().AsNew().Build(),
            ItemDtoBuilder.ToChop().AsNew().Build(),
        };

        VocabListDto newListDto = ListDtoBuilder.Empty()
                                                 .AsNew()
                                                 .WithName(Guid.NewGuid().ToString())
                                                 .WithListItems(newItems)
                                                 .Build();

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            VocabListDto _ = await repository.Add(newListDto);
        }

        VocabList? listEntity = GetSingleOrDefaultWithItemsWhere(l => l.Name == newListDto.Name
                                                        && l.Description == newListDto.Description
                                                        && l.DeletedDate == null);

        Assert.NotNull(listEntity);
        Assert.True(listEntity!.CreatedDate >= TestStartTimeStamp);

        VocabListItem[] listItems = listEntity!.ListItems.ToArray();

        Assert.Equal(newItems.Length, listItems.Length);

        for (int i = 0; i < listItems.Length; i++)
        {
            Assert.True(listItems[i].CreatedDate >= TestStartTimeStamp);
        }
    }

    [Fact]
    public async void Update_ShouldUpdateListInformation()
    {
        VocabList? entityPreUpdate = GetFirstOrDefaultWithItemsWhere(li => true);
        Assert.NotNull(entityPreUpdate);

        VocabListDto updatedDto = ListDtoBuilder.WithId(entityPreUpdate!.Id)
            .WithName(Guid.NewGuid().ToString())
            .WithDescription(Guid.NewGuid().ToString())
            .Build();

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList? testEntity = GetFirstOrDefaultWithItemsWhere(l => l.Id == entityPreUpdate.Id);

        Assert.NotNull(testEntity);
        Assert.Equal(updatedDto.Name, testEntity!.Name);
        Assert.Equal(updatedDto.Description, testEntity!.Description);
    }

    [Fact]
    public async void Update_ShouldSetListDbAdminValuesCorrectly()
    {
        VocabList entityPreUpdate = GetFirstWithItemsWhere(li => true);

        VocabListDto updatedDto = ListDtoBuilder.WithId(entityPreUpdate.Id)
            .WithName(Guid.NewGuid().ToString())
            .WithDescription(Guid.NewGuid().ToString())
            .Build();

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstWithItemsWhere(l => l.Id == entityPreUpdate.Id);
        Assert.True(testList.CreatedDate <= TestStartTimeStamp, "$@Creation time stamp is before the test start time stamp.");
        Assert.True(testList.UpdatedDate >= TestStartTimeStamp, "$@Update time stamp is before the test start time stamp.");

        Assert.Null(testList.DeletedDate);
    }

    [Fact]
    public async void Update_ShouldAddUpdateAndRemoveItemsCorrectly()
    {
        VocabList entityPreUpdate = GetFirstWithItemsWhere(li => li.ListItems.Count() > 1);
        VocabListDto updatedDto = entityPreUpdate.ToDto();

        updatedDto.Name += " (updated)";

        List<VocabListItemDto> updatedListItems = updatedDto.ListItems.ToList();

        VocabListItemDto updatedItem = updatedListItems[1];
        updatedItem.English = "updated";

        VocabListItemDto removedItem = updatedListItems[0];
        updatedListItems.RemoveAt(0);

        VocabListItemDto newItem = ItemDtoBuilder.Kettle()
                                                  .AsNew()
                                                  .WithEnglish(Guid.NewGuid().ToString())
                                                  .WithListId(entityPreUpdate.Id)
                                                  .Build();

        updatedListItems.Add(newItem);
        updatedDto.ListItems = updatedListItems;

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstWithItemsWhere(l => l.Id == entityPreUpdate.Id);
        VocabListItem[] testItems = testList.ListItems.ToArray();

        VocabListItem? testRemovedItem = testItems.FirstOrDefault(li => li.Id == removedItem.Id);
        Assert.NotNull(testRemovedItem);

        for (int i = 0; i < testItems.Length; i++)
        {
            var item = testItems[i];

            if (item.English == newItem.English)
            {
                Assert.True(item.CreatedDate >= TestStartTimeStamp.AddSeconds(-1));
                Assert.Null(item.UpdatedDate);
                Assert.Null(item.DeletedDate);
            }
            else
            {
                Assert.True(item.CreatedDate < TestStartTimeStamp);
            }

            if (item.Id == updatedItem.Id)
            {
                Assert.Equal(updatedItem.English, item.English);
                Assert.True(item.UpdatedDate.HasValue && item.UpdatedDate >= TestStartTimeStamp);
            }
            else
            {
                Assert.True(item.UpdatedDate.HasValue == false || item.UpdatedDate < TestStartTimeStamp);
            }

            if (item.Id == removedItem.Id)
            {
                Assert.True(item.DeletedDate.HasValue && item.DeletedDate >= TestStartTimeStamp);
                Assert.True(item.UpdatedDate.HasValue == false || item.UpdatedDate <= TestStartTimeStamp);
            }
            else
            {
                Assert.Null(item.DeletedDate);
            }
        }
    }

    [Fact]
    public async void Update_ShouldThrowException_IfInputHasNoId()
    {
        VocabListDto updatedDto = ListDtoBuilder.WithId(null)
            .Build();

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            _ = await Assert.ThrowsAsync<UnexpectedNullIdException>(async () =>
            {
                VocabListRepositoryAsync repository = new(context);
                await repository.Update(updatedDto);
            });
        }
    }

    private VocabList? GetFirstOrDefaultWithItemsWhere(Expression<Func<VocabList, bool>> condition)
    {
        VocabList? entity;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            entity = context.VocablLists
                            .Include(l => l.ListItems)
                            .FirstOrDefault(condition);
        }
        return entity;
    }

    private VocabList GetFirstWithItemsWhere(Expression<Func<VocabList, bool>> condition)
    {
        VocabList entity;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            entity = context.VocablLists
                            .Include(l => l.ListItems)
                            .First(condition);
        }
        return entity;
    }

    private VocabList? GetSingleOrDefaultWithItemsWhere(Expression<Func<VocabList, bool>> condition)
    {
        VocabList? entity;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            entity = context.VocablLists
                            .Include(l => l.ListItems)
                            .SingleOrDefault(condition);
        }
        return entity;
    }
}