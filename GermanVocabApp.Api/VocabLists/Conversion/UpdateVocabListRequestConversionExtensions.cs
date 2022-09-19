using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class UpdateVocabListRequestConversionExtensions
{
    public static UpdateVocabListDto ToDto(this UpdateVocabListRequest updateRequest, Guid id)
    {
        return new UpdateVocabListDto()
        {
            Id = id,
            Name = updateRequest.Name,
            Description = updateRequest.Description,
            ListItems = updateRequest.ListItems.ToDtos(id),
        };
    }
}