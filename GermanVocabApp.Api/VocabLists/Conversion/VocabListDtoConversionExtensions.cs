using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class VocabListDtoConversionExtensions
{
    public static VocabListResponse ToResponse(this VocabListDto dto)
    {
        if (!dto.Id.HasValue)
        {
            throw new UnexpectedNullIdException();
        }

        return new VocabListResponse()
        {
            Id = dto.Id.Value,
            Name = dto.Name,
            Description = dto.Description,
            ListItems = dto.ListItems.ToResponses(),
        };
    }
}
