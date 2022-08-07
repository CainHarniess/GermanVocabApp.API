#nullable enable
using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Models;

internal static class VocabListDomainConversionExtensions
{
    public static IEnumerable<VocabListInfoDto> ToResponseDtos(this IEnumerable<VocabList> domainObjects)
    {
        return domainObjects.Select(vl => vl.ToResponseDto())
                            .ToArray();
    }

    public static VocabListInfoDto ToResponseDto(this VocabList domainObject)
    {
        return new VocabListInfoDto()
        {
            Id = domainObject.Id ?? default,
            Name = domainObject.Name,
            Description = domainObject.Description,
        };
    }
}
