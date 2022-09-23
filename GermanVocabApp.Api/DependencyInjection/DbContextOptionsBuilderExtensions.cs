using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GermanVocabApp.Api.DependencyInjection;

public static class DbContextOptionsBuilderExtensions
{
    internal static void ConfigureDbContext(this DbContextOptionsBuilder options, WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("GermanVocabApp");
        options.UseSqlServer(connectionString);
        options.LogTo(Console.WriteLine);
        //StreamWriter sw = new StreamWriter("EfCoreLog.txt", append: true);
        //options.LogTo(sw.WriteLine);
        options.LogTo(log => Debug.WriteLine(log));
    }
}
