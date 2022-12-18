using GermanVocabApp.DataAccess.Shared.Core;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListDtoBuilder : EntityDtoBuilder<VocabListDto, VocabListDtoBuilder>
{
    private string _name = "Test Vocab List";
    private string? _description;
    public List<VocabListItemDto> _items = new(0);

    protected override VocabListDtoBuilder Instance => this;

    public VocabListDtoBuilder WithName(string name)
    {
        _name = name;
        return Instance;
    }

    public VocabListDtoBuilder WithDescription(string description)
    {
        _description = description;
        return Instance;
    }

    public VocabListDtoBuilder WithListItems(IEnumerable<VocabListItemDto> items)
    {
        _items = items.ToList();
        return Instance;
    }

    public VocabListDtoBuilder WithListItem(VocabListItemDto item)
    {
        _items.Add(item);
        return Instance;
    }

    public override VocabListDto Build()
    {
        var item = new VocabListDto();
        ApplyValues(item);
        Clear();
        return item;
    }

    protected override void ApplyValues(VocabListDto list)
    {
        base.ApplyValues(list);
        list.Name = _name;
        list.Description = _description;
        list.ListItems = _items;
    }

    protected override void Clear()
    {
        base.Clear();
        _name = "Test Vocab List";
        _description = default;
        _items = new List<VocabListItemDto>(0);
}
}
