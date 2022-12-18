using FluentValidation;
using GermanVocabApp.Api.Validators;
using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Api.VocabLists.Conversion.Items;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.EntityFramework;
using GermanVocabApp.DataAccess.EntityFramework.Vocab;
using GermanVocabApp.DataAccess.Shared.Vocab;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GermanVocabApp.Api.DependencyInjection;

public static class IServiceCollectionInjectionExtensions
{
    public static IServiceCollection AddValidationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<FluentNounValidator>();
        services.AddSingleton<FluentVerbValidator>();
        services.AddSingleton<FluentModifierValidator>();

        services.AddSingleton<IValidator<ListRequest>, FluentListValidator>();
        services.AddSingleton<IFactory<IValidator<ItemRequest>, ItemRequest>, WordValidatorFactory>();

        services.AddSingleton<IAggregateValidator<ItemRequest>, AggregateListItemValidator>();

        services.AddSingleton<IValidator<ListRequest>, FluentListValidator>();

        services.AddSingleton<IValidationController<ListRequest>, VocabListValidationController>();
        return services;
    }

    public static IServiceCollection AddConversionDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IConverter<VocabListItemDto, ItemResponse>, ItemDtoToResponseConverter>();
        services.AddSingleton<IConverter<VocabListItemDto[], ItemResponse[]>, AggregateConverter<VocabListItemDto, ItemResponse>>();
        services.AddSingleton<IConverter<VocabListDto, ListResponse>, ListDtoToResponseConverter>();

        services.AddSingleton<IConverter<VocabListInfoDto, ListInfoResponse>, ListInfoDtoToResponseCoverter>();
        services.AddSingleton<IConverter<VocabListInfoDto[], ListInfoResponse[]>, AggregateConverter<VocabListInfoDto, ListInfoResponse>>();

        services.AddSingleton<IConverter<ItemRequest, VocabListItemDto>, CreateItemRequestToDtoConverter>();
        services.AddSingleton<IConverter<ItemRequest[], VocabListItemDto[]>, AggregateConverter<ItemRequest, VocabListItemDto>>();
        services.AddSingleton<IConverter<ListRequest, VocabListDto>, CreateListRequestToDtoConverter>();

        services.AddSingleton<IChildResourceConverter<ItemRequest, VocabListItemDto>, UpdateItemRequestToDtoConverter>();
        services.AddSingleton<IChildResourceConverter<ItemRequest[], VocabListItemDto[]>, AggregateChildResourceConverter<ItemRequest, VocabListItemDto>>();
        services.AddSingleton<IUpdateResourceConverter<ListRequest, VocabListDto>, UpdateListRequestToDtoConverter>();

        return services;
    }

    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services)
    {
        services.AddScoped<IVocabListRepositoryAsync, VocabListRepositoryAsync>();
        services.AddScoped<IUserRepositoryAsync, UserRepositoryAsync>();
        return services;
    }

    public static IServiceCollection AddEntityFrameworkDependencies(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<GermanAppAppDbContext>(options =>
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
