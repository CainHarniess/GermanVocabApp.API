#nullable enable
using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Models;

internal static class VocabListDomainConversionExtensions
{
    public static IEnumerable<VocabListResponseDto> ToResponseDtos(this IEnumerable<VocabList> domainObjects)
    {
        return domainObjects.Select(vl => vl.ToResponseDto())
                            .ToArray();
    }

    public static VocabListResponseDto ToResponseDto(this VocabList domainObject)
    {
        return new VocabListResponseDto()
        {
            Id = domainObject.Id ?? default,
            Name = domainObject.Name,
            Description = domainObject.Description,
        };
    }
}
