using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Models.Builders;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Linq.Expressions;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class ListRepositoryTests
{
    private readonly DbContextOptions _contextOptions;
    private readonly InMemoryVocabDatabaseSeeder _dataSeeder;
    private readonly VocabListItemBuilder _itemBuilder;
    private readonly VocabListBuilder _listBuilder;
    private readonly VocabListItemDtoBuilder _itemDtoBuilder;
    private readonly VocabListDtoBuilder _listDtoBuilder;

    private readonly DateTime _testStartTimeStamp;

    public ListRepositoryTests()
    {
        _contextOptions = new DbContextOptionsBuilder().NewContextConfiguration();

        _itemBuilder = new VocabListItemBuilder();
        _listBuilder = new VocabListBuilder(_itemBuilder);
        _dataSeeder = new InMemoryVocabDatabaseSeeder(_contextOptions, _listBuilder);
        _dataSeeder.Seed();

        _itemDtoBuilder = new();
        _listDtoBuilder = new(_itemDtoBuilder);

        _testStartTimeStamp = DateTime.UtcNow;
    }

    [Fact]
    public async void Add_ShouldAddNewList_AndSetCreatedDate()
    {
        VocabListDto newListDto = _listDtoBuilder.Empty()
                                                 .AsNew()
                                                 .WithName(Guid.NewGuid().ToString())
                                                 .Build();

        VocabList? entity = await AddAndRetreiveVocabListWithItems(newListDto);

        Assert.NotNull(entity);
        Assert.True(entity!.CreatedDate >= _testStartTimeStamp);
    }

    [Fact]
    public async void Add_ShouldAddListItems_AndSetCreatedDate()
    {
        VocabListItemDto[] newItems = new VocabListItemDto[]
        {
            _itemDtoBuilder.Spicy().AsNew().Build(),
            _itemDtoBuilder.ToChop().AsNew().Build(),
        };

        VocabListDto newListDto = _listDtoBuilder.Empty()
                                                 .AsNew()
                                                 .WithName(Guid.NewGuid().ToString())
                                                 .WithListItems(newItems)
                                                 .Build();

        VocabList? entity = await AddAndRetreiveVocabListWithItems(newListDto);
        Assert.NotNull(entity);

        VocabListItem[] listItems = entity!.ListItems.ToArray();

        Assert.Equal(newItems.Length, listItems.Length);

        for (int i = 0; i < listItems.Length; i++)
        {
            Assert.True(listItems[i].CreatedDate >= _testStartTimeStamp);
        }
    }

    [Fact]
    public async void Update_ShouldUpdateListInformation()
    {
        VocabList? entityPreUpdate = GetFirstOrDefaultWithItemsWhere(li => true);
        Assert.NotNull(entityPreUpdate);

        VocabListDto updatedDto = _listDtoBuilder.WithId(entityPreUpdate!.Id)
            .WithName(Guid.NewGuid().ToString())
            .WithDescription(Guid.NewGuid().ToString())
            .Build();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
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

        VocabListDto updatedDto = _listDtoBuilder.WithId(entityPreUpdate.Id)
            .WithName(Guid.NewGuid().ToString())
            .WithDescription(Guid.NewGuid().ToString())
            .Build();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstWithItemsWhere(l => l.Id == entityPreUpdate.Id);
        Assert.True(testList.CreatedDate <= _testStartTimeStamp, "$@Creation time stamp is before the test start time stamp.");
        Assert.True(testList.UpdatedDate >= _testStartTimeStamp, "$@Update time stamp is before the test start time stamp.");

        Assert.Null(testList.DeletedDate);
    }

    [Fact(Skip = "Not sure if we want this.")]
    public async void Update_ShouldNotUpdateDbAdminValues_IfNotUpdated()
    {
        VocabList entityPreUpdate = GetFirstWithItemsWhere(li => true);

        VocabListDto updatedDto = entityPreUpdate.ToDto();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstWithItemsWhere(l => l.Id == entityPreUpdate.Id);

        Assert.True(testList.CreatedDate <= _testStartTimeStamp, $@"Creation time stamp is after the test start time stamp.");
        Assert.True(testList.UpdatedDate <= _testStartTimeStamp, $@"Uupdate time stamp is after the test start time stamp.");

        Assert.Null(testList.DeletedDate);
    }

    [Fact]
    public async void Update_ShouldUpdateItemInformation()
    {
        VocabList entityPreUpdate = GetFirstWithItemsWhere(li => true);

        VocabListDto updatedDto = entityPreUpdate.ToDto();
        VocabListItemDto[] updatedItemDtos = updatedDto.ListItems.ToArray();
        updatedDto.ListItems = updatedItemDtos;

        string suffix = " (updated)";
        for (int i = 0; i < updatedItemDtos.Length; i++)
        {
            VocabListItemDto item = updatedItemDtos[i];
            updatedItemDtos[i].English = $"{item.English}{suffix}";
        }

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstWithItemsWhere(l => l.Id == entityPreUpdate.Id);
        VocabListItem[] testItems = testList.ListItems.ToArray();

        for (int i = 0; i < testItems.Length; i++)
        {
            var item = testItems[i];
            Assert.EndsWith(suffix, item.English);
            Assert.True(item.UpdatedDate >= _testStartTimeStamp, $@"Item ""{item.English}"" at index {i} update time stamp is before the test start time stamp.");
            Assert.Null(testItems[i].DeletedDate);
        }
    }

    [Fact]
    public async void Update_ShouldSoftDeleteItemsCorrectly()
    {
        VocabList entityPreUpdate = GetFirstWithItemsWhere(li => li.ListItems.Count() > 1);
        VocabListDto updatedDto = entityPreUpdate.ToDto();

        List<VocabListItemDto> updatedListItems = updatedDto.ListItems.ToList();
        VocabListItemDto removedDto = updatedListItems[1];

        if (!removedDto.Id.HasValue)
        {
            throw new UnexpectedNullIdException();
        }

        updatedListItems.RemoveAt(1);
        updatedDto.ListItems = updatedListItems;

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstWithItemsWhere(l => l.Id == entityPreUpdate.Id);
        VocabListItem[] testItems = testList.ListItems.ToArray();

        for (int i = 0; i < testItems.Length; i++)
        {
            var item = testItems[i];
            if (item.Id == removedDto.Id.Value)
            {
                Assert.True(item.DeletedDate >= _testStartTimeStamp);
                continue;
            }
            Assert.Null(item.DeletedDate);

        }
    }


    [Fact]
    public async void Update_ShouldAddAndUpdateItemsCorrectly()
    {
        VocabList entityPreUpdate = GetFirstWithItemsWhere(li => li.ListItems.Count() > 1);
        VocabListDto updatedDto = entityPreUpdate.ToDto();

        List<VocabListItemDto> updatedListItems = updatedDto.ListItems.ToList();

        VocabListItemDto updatedItem = updatedListItems[1];
        updatedItem.English = "updated";

        VocabListItemDto removedItem = updatedListItems[0];
        updatedListItems.RemoveAt(0);

        VocabListItemDto newItem = _itemDtoBuilder.Kettle()
            .AsNew().WithListId(entityPreUpdate.Id).Build();

        updatedListItems.Add(newItem);
        updatedDto.ListItems = updatedListItems;

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            await repository.Update(updatedDto);
        }

        VocabList testList = GetFirstWithItemsWhere(l => l.Id == entityPreUpdate.Id);
        VocabListItem[] testItems = testList.ListItems.ToArray();

        for (int i = 0; i < testItems.Length; i++)
        {
            var item = testItems[i];
            Assert.True(item.CreatedDate <= _testStartTimeStamp);

            if (item.English == newItem.English)
            {
                Assert.False(item.CreatedDate >= _testStartTimeStamp);
                Assert.Null(item.UpdatedDate);
                //Assert.Null(item.DeletedDate);
            }
            else
            {
                Assert.True(item.CreatedDate < _testStartTimeStamp);
            }

            if (item.Id == updatedItem.Id)
            {
                Assert.Equal(updatedItem.English, item.English);
                Assert.True(item.UpdatedDate.HasValue && item.UpdatedDate >= _testStartTimeStamp);
            }
            else
            {
                Assert.True(item.UpdatedDate.HasValue == false || item.UpdatedDate < _testStartTimeStamp);
            }

            if (item.Id == removedItem.Id)
            {
                Assert.True(item.DeletedDate.HasValue && item.DeletedDate >= _testStartTimeStamp);
                Assert.True(item.UpdatedDate.HasValue == false || item.UpdatedDate <= _testStartTimeStamp);
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
        VocabListDto updatedDto = _listDtoBuilder.WithId(null)
            .Build();

        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            _ = await Assert.ThrowsAsync<UnexpectedNullIdException>(async () =>
            {
                VocabListRepositoryAsync repository = new(context);
                await repository.Update(updatedDto);
            });
        }
    }

    













    private async Task<VocabList?> AddAndRetreiveVocabListWithItems(VocabListDto listDto)
    {
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            VocabListDto _ = await repository.Add(listDto);
        }

        VocabList? listEntity = GetSingleOrDefaultWithItemsWhere(l => l.Name == listDto.Name
                                                        && l.Description == listDto.Description
                                                        && l.DeletedDate == null);
        return listEntity;
    }

    private VocabList? GetFirstOrDefaultWithItemsWhere(Expression<Func<VocabList, bool>> condition)
    {
        VocabList? entity;
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
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
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
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
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            entity = context.VocablLists
                            .Include(l => l.ListItems)
                            .SingleOrDefault(condition);
        }
        return entity;
    }
}