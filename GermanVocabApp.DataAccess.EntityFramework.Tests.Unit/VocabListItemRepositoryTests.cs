using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Models.Builders;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class VocabListItemRepositoryTests
{
    private readonly DbContextOptions _contextOptions;
    private readonly InMemoryVocabDatabaseSeeder _dataSeeder;
    private readonly VocabListItemBuilder _itemBuilder;
    private readonly VocabListBuilder _listBuilder;

    public VocabListItemRepositoryTests()
    {
        _contextOptions = new DbContextOptionsBuilder().NewContextConfiguration();
        _itemBuilder = new VocabListItemBuilder();
        _listBuilder = new VocabListBuilder(_itemBuilder);
        _dataSeeder = new InMemoryVocabDatabaseSeeder(_contextOptions, _listBuilder);

        _dataSeeder.Seed();
    }

    [Fact]
    public async void AddRange_ShouldAddAllEntities()
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;
        VocabListItemDtoBuilder itemDtoBuilder = new();
        VocabListItemDto[] newItems = new VocabListItemDto[2];
        newItems[0] = itemDtoBuilder.Knife().Build();
        newItems[1] = itemDtoBuilder.ToChop().Build();

        VocabList newList;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            newList = new VocabListBuilder().WithName("My New List").Build();
            await context.VocablLists.AddAsync(newList);
            await context.SaveChangesAsync();
        }

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);
            await repository.AddRangeAsync(newItems, newList.Id, transactionTimeStamp);
            await context.SaveChangesAsync();
        }

        Dictionary<string, VocabListItem> entities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            entities = context.VocablListItems
                              .Where(li => li.VocabListId == newList.Id)
                              .ToDictionary(li => li.English);
        }

        Assert.Equal(newItems.Length, entities.Count);

        for (int i = 0; i < newItems.Length; i++)
        {
            Assert.True(entities.ContainsKey(newItems[i].English));
        }

        Assert.True(entities.Values.All(li => li.CreatedDate == transactionTimeStamp));
    }

    [Fact]
    public async void Update_SetsAllDeletedTimeStampsCorrectly_IfAllItemsDeleted()
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;

        VocabList list;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            list = context.VocablLists
                          .Include(l => l.ListItems)
                          .First(l => l.DeletedDate == null);

            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);
            repository.Update(list, Array.Empty<VocabListItemDto>(), transactionTimeStamp);

            await context.SaveChangesAsync();
        }

        VocabListItem[] testEntities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            testEntities = context.VocablListItems
                                  .Where(li => li.VocabListId == list.Id)
                                  .ToArray();
        }

        Assert.Equal(list.ListItems.Count(), testEntities.Length);
        Assert.True(testEntities.All(li => li.DeletedDate == transactionTimeStamp));
    }

    [Fact]
    public async void Update_SetsDeletedDate_IfItemsPartiallyDeleted()
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;

        VocabList list;
        VocabListItemDto[] updateDto;
        VocabListItemDto[] deletedDtos;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            list = context.VocablLists
                          .Include(l => l.ListItems)
                          .First();

            VocabListItem[] listItems = list.ListItems.ToArray();
            Assert.True(listItems.Length >= 4);

            updateDto = new[] { listItems[0].ToDto(), listItems[2].ToDto(), };
            deletedDtos = new[] { listItems[1].ToDto(), listItems[3].ToDto(), };

            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);
            repository.Update(list, updateDto, transactionTimeStamp);
            await context.SaveChangesAsync();
        }

        VocabListItem[] testEntities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            testEntities = context.VocablListItems
                                  .Where(li => li.VocabListId == list.Id)
                                  .ToArray();
        }

        Assert.True(testEntities.Where(li => updateDto.Any(dto => dto.Id == li.Id))
                                .All(li => li.DeletedDate == null));

        Assert.True(testEntities.Where(li => deletedDtos.Any(dto => dto.Id == li.Id))
                                .All(li => li.DeletedDate == transactionTimeStamp));
    }

    [Fact]
    public async void Update_AddsNewItems()
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;

        VocabList list;
        VocabListItem[] existingItems;
        VocabListItemDto[] newItems;
        VocabListItemDto[] updatedItems;
        VocabListItemDtoBuilder itemDtoBuilder = new();
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            list = context.VocablLists
                          .Include(l => l.ListItems)
                          .First(l => l.DeletedDate == null);

            existingItems = list.ListItems.ToArray();
            newItems = new[] { itemDtoBuilder.Knife().Build(), itemDtoBuilder.ToChop().Build(), };
            updatedItems = new VocabListItemDto[existingItems.Length + 2];
            for (int i = 0; i < existingItems.Length; i++)
            {
                updatedItems[i] = existingItems[i].ToDto();
            }
            updatedItems[^1] = newItems[0];
            updatedItems[^2] = newItems[1];

            ItemRepositoryAsync repository = new ItemRepositoryAsync(context);
            repository.Update(list, updatedItems, transactionTimeStamp);
            await context.SaveChangesAsync();
        }

        VocabListItem[] itemsPostUpdate;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            itemsPostUpdate = context.VocablListItems
                                     .Where(i => i.VocabListId == list.Id
                                              && i.DeletedDate == null)
                                     .ToArray();
        }

        Assert.Equal(updatedItems.Length, itemsPostUpdate.Length);

        for (int i = 0; i < existingItems.Length; i++)
        {
            VocabListItem existingItem = existingItems[i];
            VocabListItem? existingItemPostUpdate = Array.Find(itemsPostUpdate, li => li.Id == existingItems[i].Id);

            Assert.NotNull(existingItemPostUpdate);
            Assert.True(existingItemPostUpdate!.CreatedDate < transactionTimeStamp);
        }

        for (int i = 1; i <= 2; i++)
        {
            VocabListItem? newItem = Array.Find(itemsPostUpdate, li => li.English == updatedItems[^1].English);
            Assert.NotNull(newItem);
            Assert.Equal(newItem!.CreatedDate, transactionTimeStamp);
        }
    }

    [Fact(Skip = "Not yet implemented.")]
    public void Update_ShouldNotSetUpdatedDate_IfItemNotUpdated()
    {
        Assert.True(false);
    }

    [Fact]
    public async void Update_ShouldUpdateExistingItems()
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;
        VocabList list;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            list = context.VocablLists
                          .Include(l => l.ListItems)
                          .First();
        }

        VocabListItemDto[] listItemSnapshot = list.ListItems
                                                  .ToDtos()
                                                  .ToArray();
        VocabListItemDto[] updateDtos = list.ListItems
                                            .ToDtos()
                                            .ToArray();
        string suffix = " (updated)";
        for (int i = 0; i < updateDtos.Length; i++)
        {
            updateDtos[i].English = $"{updateDtos[i].English}{suffix}";
        }

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabList testEntity = context.VocablLists
                                          .Include(l => l.ListItems)
                                          .First(l => l.Id == list.Id);
            ItemRepositoryAsync repository = new(context);
            repository.Update(testEntity, updateDtos, transactionTimeStamp);
            await context.SaveChangesAsync();
        }

        Dictionary<Guid, VocabListItem> testEntities;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            testEntities = context.VocablListItems
                                  .Where(li => li.VocabListId == list.Id)
                                  .ToDictionary(li => li.Id);
        }

        Assert.Equal(listItemSnapshot.Length, testEntities.Count);

        for (int i = 0; i < listItemSnapshot.Length; i++)
        {
            string expected = $"{listItemSnapshot[i].English}{suffix}";
            var snapshot = listItemSnapshot[i];
            if (!snapshot.Id.HasValue)
            {
                throw new UnexpectedNullIdException();
            }
            VocabListItem testEntity = testEntities[snapshot.Id.Value];

            string actual = testEntity.English;
            Assert.Equal(expected, actual);
            Assert.Equal(testEntity.UpdatedDate, transactionTimeStamp);
        }
    }
}
