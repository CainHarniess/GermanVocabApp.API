namespace GermanVocabApp.DataAccess.Shared.DataTransfer;
public class VocabListDto : EntityDtoBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual IEnumerable<VocabListItemDto> ListItems { get; set; }
}
