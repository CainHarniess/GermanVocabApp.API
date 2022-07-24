using GermanVocabApp.Domain.Core;

namespace GermanVocabApp.Domain.VocabListAggregate;

public class VocabList : Entity, IVocabList
{
    public string Name { get; set; }

    public string Description { get; set; }

    public List<VocabListItem> ListItems { get; set; }

    public void Add(VocabListItem listItemToAdd)
    {
        if (ListItems.Any(li => li.Id == listItemToAdd.Id))
        {
            throw new ArgumentException($"List item with ID {listItemToAdd.Id} already exists in list.", nameof(listItemToAdd));
        }
        ListItems.Add(listItemToAdd);
    }

    public void Edit(VocabListItem updatedListItem)
    {
        int itemIndex = FindItemIndex(updatedListItem.Id ?? default);

        ListItems[itemIndex] = updatedListItem;
    }

    public void Remove(Guid itemId)
    {
        int itemIndex = FindItemIndex(itemId);

        ListItems.RemoveAt(itemIndex);
    }

    private int FindItemIndex(Guid itemId)
    {
        int itemIndex = ListItems.FindIndex(li => li.Id == itemId);

        if (itemIndex == -1)
        {
            throw new ArgumentException($"List item with ID {itemId} not found in vocab list.", nameof(itemId));
        }

        return itemIndex;
    }
}
