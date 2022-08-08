#nullable enable
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class VocabListDomainConversionExtensions
{
    public static IEnumerable<VocabListInfoResponse> ToResponseDtos(this IEnumerable<VocabList> domainObjects)
    {
        return domainObjects.Select(vl => vl.ToResponseDto())
                            .ToArray();
    }

    public static VocabListInfoResponse ToResponseDto(this VocabList domainObject)
    {
        return new VocabListInfoResponse()
        {
            Id = domainObject.Id ?? default,
            Name = domainObject.Name,
            Description = domainObject.Description,
        };
    }
}
