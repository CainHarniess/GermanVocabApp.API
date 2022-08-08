using GermanVocabApp.Api.VocabLists.Models;
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
        return new VocabListInfoResponse()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
        };
    }
}
