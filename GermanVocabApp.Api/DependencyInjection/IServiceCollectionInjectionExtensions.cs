﻿using FluentValidation;
using GermanVocabApp.Api.FluentValidation;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.EntityFramework;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
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

        services.AddSingleton<IConverter<VocabListInfoDto, ListInfoResponse>, ListInfoDtoToResponseCoverter>();
        services.AddSingleton<IConverter<VocabListInfoDto[], ListInfoResponse[]>, AggregateConverter<VocabListInfoDto, ListInfoResponse>>();

        services.AddSingleton<IConverter<VocabListDto, ListResponse>, ListDtoToResponseConverter>();
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
