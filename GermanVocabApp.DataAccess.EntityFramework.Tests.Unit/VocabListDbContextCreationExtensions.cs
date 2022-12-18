using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public static class VocabListDbContextCreationExtensions
{
    public static DbContextOptions NewContextConfiguration(this DbContextOptionsBuilder builder)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
        var connection = new SqliteConnection(connectionStringBuilder.ToString());
        return builder.UseSqlite(connection)
                      .Options;

    }

    public static GermanAppAppDbContext BuildNewInMemoryContext(this DbContextOptions options)
    {
        return new GermanAppAppDbContext(options);
    }
}