using AutoFixture;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Models.Builders;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class VocabListItemRepositoryTests
{
    private Fixture _fixture;
    private DbContextOptions _contextOptions;

    public VocabListItemRepositoryTests()
    {
        _fixture = new Fixture();
        _contextOptions = new DbContextOptionsBuilder().NewContextConfiguration();
    }

    [Fact]
    public async void AddRange_ShouldAddAllEntities()
    {
        VocabList listEntity = new VocabListBuilder().Default().Build();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.VocablLists.Add(listEntity);
            await context.SaveChangesAsync();
        }

        VocabListItemDto item1 = _fixture.Create<VocabListItemDto>();
        item1.Id = null;
        item1.VocabListId = null;

        VocabListItemDto item2 = _fixture.Create<VocabListItemDto>();
        item2.Id = null;
        item2.VocabListId = null;

        VocabListItemDto[] itemsToAdd = new VocabListItemDto[] { item1, item2 };

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);
            await repository.AddRangeAsync(itemsToAdd, listEntity.Id, DateTime.UtcNow);
            await context.SaveChangesAsync();
        }

        Dictionary<string, VocabListItem> entities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            entities = context.VocablListItems.ToDictionary(li => li.English);
        }

        Assert.Equal(itemsToAdd.Length, entities.Count);

        for (int i = 0; i < itemsToAdd.Length; i++)
        {
            var item = itemsToAdd[i];
            Assert.True(entities.ContainsKey(item.English));
        }
    }

    [Fact]
    public async void Update_SetsAllDeletedTimeStampsCorrectly()
    {
        DateTime expectedTimeStamp = DateTime.UtcNow;
        
        VocabListBuilder listBuilder = new VocabListBuilder();
        VocabList list = listBuilder.Default().Build();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.VocablLists.Add(list);
            await context.SaveChangesAsync();
        }

        var itemBuilder = new VocabListItemBuilder();
        VocabListItem item1 = itemBuilder.BasicWithListId(list.Id).Build();
        VocabListItem item2 = itemBuilder.BasicWithListId(list.Id).Build();
        VocabListItem[] items = new VocabListItem[] { item1, item2 };
        
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.VocablListItems.AddRange(items);
            await context.SaveChangesAsync();
        }

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabList listEntity = context.VocablLists
                                          .Where(l => l.Id == list.Id)
                                          .Include(l => l.ListItems)
                                          .First();

            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);
            await repository.UpdateAsync(listEntity, Array.Empty<VocabListItemDto>(), expectedTimeStamp);
            await context.SaveChangesAsync();
        }

        VocabListItem[] testEntities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            testEntities = context.VocablListItems.Where(li => li.VocabListId == list.Id).ToArray();
        }

        Assert.Equal(items.Length, testEntities.Length);
        Assert.True(testEntities.All(li => li.DeletedDate == expectedTimeStamp));
    }

    [Fact]
    public async void Update_SetsDeletedDate_IfItemDeleted()
    {
        DateTime expectedTimeStamp = DateTime.UtcNow;

        VocabListBuilder listBuilder = new VocabListBuilder();
        VocabList list = listBuilder.Default().Build();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.VocablLists.Add(list);
            await context.SaveChangesAsync();
        }

        var itemBuilder = new VocabListItemBuilder();

        VocabListItem item1 = itemBuilder.BasicWithListId(list.Id).Build();
        VocabListItem item2 = itemBuilder.BasicWithListId(list.Id).Build();
        VocabListItem item3 = itemBuilder.BasicWithListId(list.Id).Build();
        VocabListItem item4 = itemBuilder.BasicWithListId(list.Id).Build();

        VocabListItem[] items = new VocabListItem[] { item1, item2, item3, item4 };

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.VocablListItems.AddRange(items);
            await context.SaveChangesAsync();
        }

        VocabListItemDto[] updateItems = new VocabListItemDto[]
        {
            new VocabListItemDto() { Id = item1.Id, },
            new VocabListItemDto() { Id = item3.Id, },
        };

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabList listEntity = context.VocablLists
                                          .Where(l => l.Id == list.Id)
                                          .Include(l => l.ListItems)
                                          .First();

            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);

            await repository.UpdateAsync(listEntity, updateItems, expectedTimeStamp);
            await context.SaveChangesAsync();
        }

        VocabListItem[] testEntities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            testEntities = context.VocablListItems.Where(li => li.VocabListId == list.Id).ToArray();
        }

        Assert.True(testEntities.Where(li => li.Id == item1.Id || li.Id == item3.Id)
                                .All(li => li.DeletedDate == null));

        Assert.True(testEntities.Where(li => li.Id != item1.Id && li.Id != item3.Id)
                                .All(li => li.DeletedDate == expectedTimeStamp));
    }

    [Fact]
    public async void Update_AddsNewItems()
    {
        DateTime expectedTimeStamp = DateTime.UtcNow;

        VocabListBuilder listBuilder = new VocabListBuilder();
        VocabList localList = listBuilder.Default().Build();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            context.VocablLists.Add(localList);
            await context.SaveChangesAsync();
        }

        var itemBuilder = new VocabListItemBuilder();

        VocabListItem item1 = itemBuilder.BasicWithListId(localList.Id)
                                         .WithEnglish(Guid.NewGuid().ToString())
                                         .Build();
        VocabListItem item2 = itemBuilder.BasicWithListId(localList.Id)
                                         .WithEnglish(Guid.NewGuid().ToString())
                                         .Build();
        VocabListItem[] items = new VocabListItem[] { item1, item2 };

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.VocablListItems.AddRange(items);
            await context.SaveChangesAsync();
        }

        VocabListItemDto item3Dto = _fixture.Create<VocabListItemDto>();
        item3Dto.Id = null;
        item3Dto.VocabListId = localList.Id;
        VocabListItemDto item4Dto = _fixture.Create<VocabListItemDto>();
        item4Dto.Id = null;
        item4Dto.VocabListId = localList.Id;

        VocabListItemDto[] updatedItems = new VocabListItemDto[]
        {
            new VocabListItemDto() { Id = item1.Id, VocabListId = localList.Id, English = item1.English },
            item3Dto,
            item4Dto,
        };

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabList listEntity = context.VocablLists
                                          .Where(l => l.Id == localList.Id)
                                          .Include(l => l.ListItems)
                                          .First();

            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);

            await repository.UpdateAsync(listEntity, updatedItems, expectedTimeStamp);
            await context.SaveChangesAsync();
        }

        Dictionary<string, VocabListItem> entities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            entities = context.VocablListItems
                              .Where(i => i.VocabListId == localList.Id
                                       && i.DeletedDate == null)
                              .ToDictionary(li => li.English);
        }

        Assert.Equal(updatedItems.Length, entities.Count);

        for (int i = 0; i < updatedItems.Length; i++)
        {
            var item = updatedItems[i];
            Assert.True(entities.ContainsKey(item.English));
        }
        Assert.False(entities.ContainsKey(item2.English));

    }
}