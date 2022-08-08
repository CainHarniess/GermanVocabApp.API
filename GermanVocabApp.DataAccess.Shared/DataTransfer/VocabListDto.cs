namespace GermanVocabApp.DataAccess.Shared.DataTransfer;
public class VocabListDto : VocabListInfoDto
{
    public virtual IEnumerable<VocabListItemDto> ListItems { get; set; }
}
