using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabLists;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared;

namespace GermanVocabApp.Api.DependencyInjection;

public static class IServiceCollectionExtensions
{
    internal static IServiceCollection ConfigureInjections(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateVocabListRequest>, CreateListRequestValidator>();
        services.AddScoped<IValidator<UpdateVocabListRequest>, UpdateListRequestValidator>();
        services.AddScoped<IVocabListRepositoryAsync, VocabListRepositoryAsync>();
        return services;
        
    }
}