using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Domain.Abstractions
{
    public interface IVocabListRepositoryAsync
    {
        Task<IEnumerable<VocabList>> GetAll();

        Task<VocabList?> Get(Guid listId);

        Task Add(VocabList list);

        Task Edit(VocabList list);
    }
}
