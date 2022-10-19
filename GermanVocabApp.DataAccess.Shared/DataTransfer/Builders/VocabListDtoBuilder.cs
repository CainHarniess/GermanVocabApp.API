using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListDtoBuilder : EntityDtoBuilder<VocabListDto, VocabListDtoBuilder>
{
    private readonly VocabListItemDtoBuilder _itemBuilder;

    public VocabListDtoBuilder(VocabListItemDtoBuilder itemBuilder)
    {
        _itemBuilder = itemBuilder;
    }

    public VocabListDtoBuilder()
    {
        _itemBuilder = new VocabListItemDtoBuilder();
    }

    public VocabListDtoBuilder Default()
    {
        return this;
    }

    public override VocabListDtoBuilder AsNew()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Id = null;
        }
        return base.AsNew();
    }

    public VocabListDtoBuilder Empty()
    {
        return WithId(Guid.NewGuid())
              .WithName("Empty Vocab List")
              .WithDescription("This vocab list contains no items");
    }

    public VocabListDtoBuilder Kitchen()
    {
        Guid listId = Guid.NewGuid();
        DateTime creationTimestamp = DateTime.UtcNow;

        VocabListItemDto[] items = new VocabListItemDto[]
        {
            _itemBuilder.Kettle(listId).Build(),
            _itemBuilder.ToCook(listId).Build(),
            _itemBuilder.Spicy(listId).Build(),
            _itemBuilder.Saucily(listId).Build(),
        };

        return WithId(listId)
              .WithName("Kitchen")
              .WithDescription("A culinary collection.")
              .WithListItems(items);
    }
}
