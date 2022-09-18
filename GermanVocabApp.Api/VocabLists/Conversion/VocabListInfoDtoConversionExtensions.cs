using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class VocabListInfoDtoConversionExtensions
{
    public static IEnumerable<VocabListInfoResponse> ToResponses(this IEnumerable<VocabListInfoDto> dtos)
    {
        return dtos.Select(dto => dto.ToResponse());
    }

    public static VocabListInfoResponse ToResponse(this VocabListInfoDto dto)
    {
        if (!dto.Id.HasValue)
        {
            throw new UnexpectedNullIdException();
        }
        return new VocabListInfoResponse()
        {
            Id = dto.Id.Value,
            Name = dto.Name,
            Description = dto.Description,
        };
    }
}
