using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.Shared
{
    public interface INewVocabListRepositoryAsync
    {
        Task<IEnumerable<VocabListInfoDto>>
            GetVocabListInfos();

        Task<VocabListDto?> Get(Guid listId);
    }
}
