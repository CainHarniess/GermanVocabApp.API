using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.Shared
{
    public interface IVocabListRepositoryAsync
    {
        Task<IEnumerable<VocabListInfoDto>> GetVocabListInfos();
        Task<VocabListDto?> Get(Guid id);
        Task<VocabListDto> Add(VocabListDto newList);
        Task Update(VocabListDto updatedList);
        Task<bool> HardDelete(Guid id);
    }
}
