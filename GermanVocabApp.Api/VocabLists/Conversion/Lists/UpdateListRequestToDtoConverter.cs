using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion.Lists;

public class UpdateListRequestToDtoConverter : IUpdateResourceConverter<ListRequest, VocabListDto>
{
    private readonly IChildResourceConverter<ItemRequest[], VocabListItemDto[]> _itemsConverter;

    public UpdateListRequestToDtoConverter(IChildResourceConverter<ItemRequest[], VocabListItemDto[]> itemsConverter)
    {
        _itemsConverter = itemsConverter;
    }

    public VocabListDto Convert(ListRequest source, Guid resourceId)
    {
        return new VocabListDto()
        {
            Id = resourceId,
            Name = source.Name,
            Description = source.Description,
            ListItems = _itemsConverter.Convert(source.ListItems.ToArray(), resourceId),
        };
    }
}