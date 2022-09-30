using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class ListInfoDtoConversionExtensions
{
    public static IEnumerable<ListInfoResponse> ToResponses(this IEnumerable<VocabListInfoDto> dtos)
    {
        return dtos.Select(dto => dto.ToResponse());
    }

    public static ListInfoResponse ToResponse(this VocabListInfoDto dto)
    {
        return new ListInfoResponse()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
        };
    }
}
