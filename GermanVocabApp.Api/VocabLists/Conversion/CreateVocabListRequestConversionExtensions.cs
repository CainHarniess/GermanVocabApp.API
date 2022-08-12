using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class CreateVocabListRequestConversionExtensions
{
    public static CreateVocabListDto ToDto(this CreateVocabListRequest dto)
    {
        DateTime creationTimeStamp = DateTime.UtcNow;
        return ToDto(dto, creationTimeStamp);
    }

    public static CreateVocabListDto ToDto(this CreateVocabListRequest dto, DateTime creationTimeStamp)
    {
        return new CreateVocabListDto()
        {
            Name = dto.Name,
            Description = dto.Description,
            ListItems = dto.ListItems.Select(li => li.ToDto()),
        };
    }
}