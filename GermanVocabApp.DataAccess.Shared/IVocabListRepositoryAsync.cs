using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.Shared
{
    public interface IVocabListRepositoryAsync
    {
        Task<IEnumerable<VocabListInfoDto>> GetVocabListInfos();

        Task<VocabListDto?> Get(Guid listId);

        Task<VocabListDto> Add(CreateVocabListDto newList);
    }
}
