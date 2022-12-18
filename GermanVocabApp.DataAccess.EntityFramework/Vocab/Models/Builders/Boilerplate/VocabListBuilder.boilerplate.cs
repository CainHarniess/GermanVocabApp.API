using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListBuilder : EntityBuilder<VocabList, VocabListBuilder>
{
    private string _name = "Test Vocab List";
    private string? _description;
    public List<VocabListItem> _items = new(0);

    protected override VocabListBuilder Instance => this;

    public VocabListBuilder WithName(string name)
    {
        _name = name;
        return Instance;
    }

    public VocabListBuilder WithDescription(string description)
    {
        _description = description;
        return Instance;
    }

    public VocabListBuilder WithListItems(IEnumerable<VocabListItem> items)
    {
        _items = items.ToList();
        return Instance;
    }

    public VocabListBuilder WithListItem(VocabListItem item)
    {
        _items.Add(item);
        return Instance;
    }

    public override VocabList Build()
    {
        var item = new VocabList();
        ApplyValues(item);
        Clear();
        return item;
    }

    protected override void ApplyValues(VocabList item)
    {
        base.ApplyValues(item);
        item.Name = _name;
        item.Description = _description;
        item.ListItems = _items;
    }

    protected override void Clear()
    {
        base.Clear();
        _name = default;
        _description = default;
        _items = default;
}
}
