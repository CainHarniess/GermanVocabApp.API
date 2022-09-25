using GermanVocabApp.Api.VocabLists.Validation;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.EntityFramework;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GermanVocabApp.Api.DependencyInjection;

public static class IServiceCollectionInjectionExtensions
{
    public static IServiceCollection AddValidationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<IListRequest>, VocabListRequestValidator>();
        return services;
    }

    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services)
    {
        services.AddScoped<IVocabListRepositoryAsync, VocabListRepositoryAsync>();
        return services;
    }

    public static IServiceCollection AddEntityFrameworkDependencies(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<VocabListDbContext>(options =>
        {
            string connectionString = builder.Configuration.GetConnectionString("GermanVocabApp");
            options.UseSqlServer(connectionString);
            options.LogTo(Console.WriteLine);
            //StreamWriter sw = new StreamWriter("EfCoreLog.txt", append: true);
            //options.LogTo(sw.WriteLine);
            options.LogTo(log => Debug.WriteLine(log));
        }, ServiceLifetime.Scoped);
        return services;
    }
}
