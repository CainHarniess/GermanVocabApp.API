using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class ListRequestConversionExtensions
{
    public static VocabListDto ToUpdateDto(this ListRequest updateRequest, Guid id)
    {
        return new VocabListDto()
        {
            Id = id,
            Name = updateRequest.Name,
            Description = updateRequest.Description,
            ListItems = updateRequest.ListItems.ToUpdateDtos(id),
        };
    }

    public static VocabListDto ToCreationDto(this ListRequest request)
    {
        return new VocabListDto()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            ListItems = request.ListItems.ToCreationDtos(),
        };
    }
}