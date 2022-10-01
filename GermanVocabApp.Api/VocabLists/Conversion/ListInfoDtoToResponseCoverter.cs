using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

public class ListInfoDtoToResponseCoverter : IConverter<VocabListInfoDto, ListInfoResponse>
{
    public ListInfoResponse Convert(VocabListInfoDto source)
    {
        return new ListInfoResponse()
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
        };
    }
}
