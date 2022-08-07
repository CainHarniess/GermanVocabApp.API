using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Models;

internal static class VocabListRequestDtoConversionExtensions
{
    public static VocabList ToDomainModel(this VocabListCreationDto request)
    {
        return new VocabList()
        {
            Id = null,
            Name = request.Name,
            Description = request.Description,
        };
    }
}
