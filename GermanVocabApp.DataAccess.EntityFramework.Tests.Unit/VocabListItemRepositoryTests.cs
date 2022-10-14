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
    private readonly VocabListItemBuilder _itemBuilder;
    private readonly VocabListBuilder _listBuilder;
    private readonly InMemoryVocabDatabaseSeeder _dataSeeder;

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
        VocabListItemDto[] updatedItems;
        VocabListItemDtoBuilder itemDtoBuilder = new();
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            list = context.VocablLists
                          .Include(l => l.ListItems)
                          .First(l => l.DeletedDate == null);

            existingItems = list.ListItems.ToArray();

            updatedItems = new VocabListItemDto[existingItems.Length + 2];
            for (int i = 0; i < existingItems.Length; i++)
            {
                updatedItems[i] = existingItems[i].ToDto();
            }
            updatedItems[^1] = itemDtoBuilder.Knife().Build();
            updatedItems[^2] = itemDtoBuilder.ToChop().Build();

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
            var existingItem = existingItems[i];
            Assert.NotNull(Array.Find(itemsPostUpdate, li => li.Id == existingItems[i].Id));
        }
        Assert.NotNull(Array.Find(itemsPostUpdate, li => li.English == updatedItems[^1].English));
        Assert.NotNull(Array.Find(itemsPostUpdate, li => li.English == updatedItems[^2].English));
    }
}