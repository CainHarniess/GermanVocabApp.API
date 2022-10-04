using GermanVocabApp.DataAccess.EntityFramework.Models;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;

public class VocabListBuilder : EntityBuilder<VocabList>
{
    private string _name = "Test Vocab List";
    private string? _description;
    public List<VocabListItem> _items = new List<VocabListItem>(0);

    public VocabListBuilder Default()
    {
        return this;
    }

    public VocabList DefaultWithListItems(IEnumerable<VocabListItem> items)
    {
        return WithListItems(items)
              .Build();
    }

    public override VocabList Build()
    {
        var item = new VocabList();
        ApplyBaseValues(item);

        item.Name = _name;
        item.Description = _description;
        item.ListItems = _items;
        return item;
    }

    public VocabListBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public VocabListBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public VocabListBuilder WithListItems(IEnumerable<VocabListItem> items)
    {
        _items = items.ToList();
        return this;
    }

    public VocabListBuilder WithListItem(VocabListItem item)
    {
        _items.Add(item);
        return this;
    }
}
