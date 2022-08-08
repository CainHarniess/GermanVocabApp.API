namespace GermanVocabApp.DataAccess.EntityFramework.DataTransfer;
public class VocabListDto : VocabListInfoDto
{
    public virtual IEnumerable<VocabListItemDto>? ListItems { get; set; }
}
