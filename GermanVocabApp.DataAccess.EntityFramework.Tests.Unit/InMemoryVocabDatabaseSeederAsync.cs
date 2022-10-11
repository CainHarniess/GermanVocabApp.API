using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Models.Builders;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class InMemoryVocabDatabaseSeederAsync
{
    private DbContextOptions _contextOptions;

    public InMemoryVocabDatabaseSeederAsync(DbContextOptions contextOptions)
    {
        _contextOptions = contextOptions;
    }

    public async Task SeedAsync()
    {
        using (VocabListDbContext context = _contextOptions.BuildNewInMemoryContext())
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            VocabListItemBuilder itemBuilder = new();
            VocabListBuilder listBuilder = new(itemBuilder);

            VocabList kitchen = listBuilder.Kitchen().Build();
            context.Add(kitchen);

            await context.SaveChangesAsync();
        }
    }
}
