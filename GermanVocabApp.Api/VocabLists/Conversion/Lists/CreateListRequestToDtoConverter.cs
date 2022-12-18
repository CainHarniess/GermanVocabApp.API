using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;

namespace GermanVocabApp.Api.VocabLists.Conversion.Lists;

public class CreateListRequestToDtoConverter : IConverter<ListRequest, VocabListDto>
{
    private readonly IConverter<ItemRequest[], VocabListItemDto[]> _itemsConverter;

    public CreateListRequestToDtoConverter(IConverter<ItemRequest[], VocabListItemDto[]> itemsConverter)
    {
        _itemsConverter = itemsConverter;
    }

    public VocabListDto Convert(ListRequest source)
    {
        if (source.Id.HasValue)
        {
            throw new UnexpectedIdException($"Resource creation request with ID {source.Id} but null was expected.");
        }
        return new VocabListDto()
        {
            Name = source.Name,
            Description = source.Description,
            ListItems = _itemsConverter.Convert(source.ListItems.ToArray()),
        };
    }
}