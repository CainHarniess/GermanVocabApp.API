using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class VocabListDtoConversionExtensions
{
    public static VocabListResponse ToResponse(this VocabListDto dto)
    {
        return new VocabListResponse()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            ListItems = dto.ListItems.ToResponses(),
        };
    }
}
