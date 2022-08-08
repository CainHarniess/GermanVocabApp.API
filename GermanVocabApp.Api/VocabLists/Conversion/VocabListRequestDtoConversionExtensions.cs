using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Conversion;

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
