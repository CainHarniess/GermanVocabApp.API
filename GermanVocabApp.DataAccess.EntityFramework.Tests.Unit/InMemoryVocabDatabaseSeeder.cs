using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Models.Builders;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class InMemoryVocabDatabaseSeeder
{
    private readonly DbContextOptions _contextOptions;
    private readonly VocabListBuilder _listBuilder;

    public InMemoryVocabDatabaseSeeder(DbContextOptions contextOptions,
                                            VocabListBuilder listBuilder)
    {
        _contextOptions = contextOptions;
        _listBuilder = listBuilder;
    }

    public void Seed()
    {
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            VocabList kitchen = _listBuilder.Kitchen().Build();
            context.Add(kitchen);

            context.SaveChanges();
        }
    }
}
