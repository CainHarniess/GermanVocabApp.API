using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

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
        return new VocabListDto()
        {
            Name = source.Name,
            Description = source.Description,
            ListItems = _itemsConverter.Convert(source.ListItems.ToArray()),
        };
    }
}