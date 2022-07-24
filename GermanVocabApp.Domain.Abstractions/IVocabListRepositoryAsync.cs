using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Domain.Abstractions
{
    public interface IVocabListRepositoryAsync
    {
        Task<IEnumerable<VocabList>> GetAll();

        Task<VocabList?> Get(Guid listId);

        Task Add(VocabList list);

        Task Edit(VocabList list);

        Task Remove(Guid listId);

        Task<IEnumerable<VocabListItem>> GetListItems(Guid listId);

        Task AddToList(Guid listId, VocabListItem item);

        Task EditListItem(Guid listId, VocabListItem updatedItem);

        Task RemoveFromList(Guid listId, Guid itemId);
    }
}
