using GermanVocabApp.Core.Testing;
using GermanVocabApp.DataAccess.EntityFramework.Models;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;

public partial class VocabListBuilder : AbstractBuilder<VocabList>
{
    public VocabListBuilder Default()
    {
        return this;
    }

    public VocabList DefaultWithListItems(IEnumerable<VocabListItem> items)
    {
        return WithListItems(items)
              .Build();
    }
}

public partial class VocabListBuilder
{
    private ICollection<VocabListItem> _items;
    public VocabListBuilder WithListItem(VocabListItem item)
    {
        _items.Add(item);
        return this;
    }

    public VocabListBuilder WithListItems(IEnumerable<VocabListItem> items)
    {
        _items = items.ToList();
        return this;
    }
}
