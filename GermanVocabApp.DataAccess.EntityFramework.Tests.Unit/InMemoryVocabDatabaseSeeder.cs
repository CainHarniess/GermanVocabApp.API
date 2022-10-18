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

            VocabList[] lists = new VocabList[]
            {
                _listBuilder.Kitchen().Build(),
                _listBuilder.WithName("zz_deleted").WithDeletedDate(DateTime.UtcNow).Build(),
            };
            context.AddRange(lists);

            context.SaveChanges();
        }
    }
}
