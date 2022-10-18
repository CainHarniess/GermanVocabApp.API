using GermanVocabApp.DataAccess.Models.Builders;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public abstract class ListRepositoryTestConfiguration
{
    private readonly DbContextOptions _contextOptions;
    private readonly InMemoryVocabDatabaseSeeder _dataSeeder;
    private readonly VocabListItemDtoBuilder _itemDtoBuilder;
    private readonly VocabListDtoBuilder _listDtoBuilder;

    private readonly DateTime _testStartTimeStamp;

    public ListRepositoryTestConfiguration()
    {
        _contextOptions = new DbContextOptionsBuilder().NewContextConfiguration();

        var itemBuilder = new VocabListItemBuilder();
        var listBuilder = new VocabListBuilder(itemBuilder);
        _dataSeeder = new InMemoryVocabDatabaseSeeder(_contextOptions, listBuilder);
        _dataSeeder.Seed();

        _itemDtoBuilder = new();
        _listDtoBuilder = new(_itemDtoBuilder);

        _testStartTimeStamp = DateTime.UtcNow;
    }

    protected DbContextOptions ContextOptions => _contextOptions;
    protected InMemoryVocabDatabaseSeeder DataSeeder => _dataSeeder;
    protected VocabListItemDtoBuilder ItemDtoBuilder => _itemDtoBuilder;
    protected VocabListDtoBuilder ListDtoBuilder => _listDtoBuilder;
    protected DateTime TestStartTimeStamp => _testStartTimeStamp;
}
