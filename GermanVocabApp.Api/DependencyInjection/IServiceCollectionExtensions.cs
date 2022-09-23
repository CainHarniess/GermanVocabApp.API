using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared;

namespace GermanVocabApp.Api.DependencyInjection;

public static class IServiceCollectionExtensions
{
    internal static IServiceCollection ConfigureInjections(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateVocabListRequest>, CreateVocabListRequestValidator>();
        services.AddScoped<IVocabListRepositoryAsync, VocabListRepositoryAsync>();
        return services;
        
    }
}