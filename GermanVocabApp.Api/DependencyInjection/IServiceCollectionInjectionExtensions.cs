using GermanVocabApp.Api.FluentValidation;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Api.VocabLists.Models;
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
        services.AddSingleton<WordValidatorFactory>();

        services.AddSingleton<FluentListValidator<CreateVocabListItemRequest>>();
        services.AddSingleton<FluentListValidator<UpdateVocabListItemRequest>>();

        //services.AddSingleton<VocabListValidationController<CreateVocabListItemRequest>>();
        //services.AddSingleton<VocabListValidationController<UpdateVocabListItemRequest>>();
        
        services.AddSingleton<IValidationController<IListRequest<CreateVocabListItemRequest>>, VocabListValidationController<CreateVocabListItemRequest>>();
        services.AddSingleton<IValidationController<IListRequest<UpdateVocabListItemRequest>>, VocabListValidationController<UpdateVocabListItemRequest>>();
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
