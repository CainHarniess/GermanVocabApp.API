using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion.Lists;

public class ListDtoToResponseConverter : IConverter<VocabListDto, ListResponse>
{
    private readonly IConverter<VocabListItemDto[], ItemResponse[]> _itemConverter;

    public ListDtoToResponseConverter(IConverter<VocabListItemDto[], ItemResponse[]> itemConverter)
    {
        _itemConverter = itemConverter;
    }

    public ListResponse Convert(VocabListDto dto)
    {
        if (dto.Id.HasValue == false)
        {
            throw new UnexpectedNullIdException();
        }
        return new ListResponse()
        {
            Id = dto.Id.Value,
            Name = dto.Name,
            Description = dto.Description,
            ListItems = _itemConverter.Convert(dto.ListItems.ToArray()),
        };
    }
}