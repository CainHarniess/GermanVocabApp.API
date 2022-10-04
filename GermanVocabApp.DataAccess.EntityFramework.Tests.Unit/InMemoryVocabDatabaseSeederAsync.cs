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

            await context.SaveChangesAsync();
        }
    }
}
