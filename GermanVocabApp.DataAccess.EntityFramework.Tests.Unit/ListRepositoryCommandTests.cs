﻿using GermanVocabApp.Core.Exceptions;
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

        VocabList? listEntity = GetSingleOrDefaultIncludeItemsWhere(l => l.Name == newListDto.Name
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
        VocabList? entityPreUpdate = GetFirstOrDefaultIncludeItemsWhere(li => li.DeletedDate == null);
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

        VocabList? testEntity = GetFirstOrDefaultIncludeItemsWhere(l => l.Id == entityPreUpdate.Id);

        Assert.NotNull(testEntity);
        Assert.Equal(updatedDto.Name, testEntity!.Name);
        Assert.Equal(updatedDto.Description, testEntity!.Description);
    }

    [Fact]
    public async void Update_ShouldSetListDbAdminValuesCorrectly()
    {
        VocabList entityPreUpdate = GetFirstIncludeItemsWhere(li => li.DeletedDate.HasValue == false);

        VocabListDto updatedDto = ListDtoBuilder.WithId(entityPreUpdate.Id)
            .WithName(Guid.NewGuid().ToString())
            .WithDescription(Guid.NewGuid().ToString())
            .Build();

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstIncludeItemsWhere(l => l.Id == entityPreUpdate.Id);
        Assert.True(testList.CreatedDate <= TestStartTimeStamp, "$@Creation time stamp is before the test start time stamp.");
        Assert.True(testList.UpdatedDate >= TestStartTimeStamp, "$@Update time stamp is before the test start time stamp.");

        Assert.Null(testList.DeletedDate);
    }

    [Fact]
    public async void Update_ShouldAddUpdateAndRemoveItemsCorrectly()
    {
        VocabList entityPreUpdate;
        entityPreUpdate = GetFirstActiveIncludeActiveItems();
        VocabListDto updatedDto = entityPreUpdate.ToDto();

        updatedDto.Name += " (updated)";

        ItemModificationResult modificationResult = ModifyListItems(entityPreUpdate, updatedDto);

        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList;
        using (VocabListDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            testList = context.VocablLists
                              .Include(l => l.ListItems
                                             .Where(i => i.DeletedDate == null))
                              .First(li => li.DeletedDate.HasValue == false
                                        && li.ListItems.Count() > 1);
        }
        VocabListItem[] testItems = testList.ListItems.ToArray();

        VocabListItem? testRemovedItem = testItems.FirstOrDefault(li => li.Id == modificationResult.Removed.Id);
        Assert.Null(testRemovedItem);

        for (int i = 0; i < testItems.Length; i++)
        {
            TestModifiedItems(modificationResult, testItems[i]);
        }
    }

    private void TestModifiedItems(ItemModificationResult modificationResult, VocabListItem item)
    {
        if (item.English == modificationResult.Added.English)
        {
            Assert.True(item.CreatedDate >= TestStartTimeStamp.AddSeconds(-1));
            Assert.Null(item.UpdatedDate);
            Assert.Null(item.DeletedDate);
        }
        else
        {
            Assert.True(item.CreatedDate < TestStartTimeStamp);
        }

        if (item.Id == modificationResult.Updated.Id)
        {
            Assert.Equal(modificationResult.Updated.English, item.English);
            Assert.True(item.UpdatedDate.HasValue && item.UpdatedDate >= TestStartTimeStamp);
        }
        else
        {
            Assert.True(item.UpdatedDate.HasValue == false || item.UpdatedDate < TestStartTimeStamp);
        }

        if (item.Id == modificationResult.Removed.Id)
        {
            Assert.True(item.DeletedDate.HasValue && item.DeletedDate >= TestStartTimeStamp);
            Assert.True(item.UpdatedDate.HasValue == false || item.UpdatedDate <= TestStartTimeStamp);
        }
        else
        {
            Assert.Null(item.DeletedDate);
        }
    }

    private class ItemModificationResult
    {
        public ItemModificationResult(VocabListItemDto added, VocabListItemDto updated,
                                 VocabListItemDto removed)
        {
            Added = added;
            Updated = updated;
            Removed = removed;
        }

        public VocabListItemDto Added { get; }
        public VocabListItemDto Updated { get; }
        public VocabListItemDto Removed { get; }
    }

    private ItemModificationResult ModifyListItems(VocabList entityPreUpdate, VocabListDto updatedDto)
    {
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

        return new ItemModificationResult(newItem, updatedItem, removedItem);
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
            testList = context.VocablLists
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
            itemIds = context.VocablListItems
                             .Where(i => i.VocabListId == listId)
                             .Select(i => i.Id)
                             .ToArray();
        }
        Assert.Empty(itemIds);
    }


    private VocabList? GetFirstOrDefaultIncludeItemsWhere(Expression<Func<VocabList, bool>> condition)
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

    private VocabList GetFirstIncludeItemsWhere(Expression<Func<VocabList, bool>> condition)
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

    private VocabList? GetSingleOrDefaultIncludeItemsWhere(Expression<Func<VocabList, bool>> condition)
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