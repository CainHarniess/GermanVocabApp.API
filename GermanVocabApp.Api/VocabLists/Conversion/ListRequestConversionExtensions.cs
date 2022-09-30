using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class ListRequestConversionExtensions
{
    public static VocabListDto ToDto(this ListRequest updateRequest, Guid id)
    {
        return new VocabListDto()
        {
            Id = id,
            Name = updateRequest.Name,
            Description = updateRequest.Description,
            ListItems = updateRequest.ListItems.ToDtos(id),
        };
    }

    public static VocabListDto ToDto(this ListRequest request)
    {
        return new VocabListDto()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            ListItems = request.ListItems.ToDtos(request.Id),
        };
    }
}