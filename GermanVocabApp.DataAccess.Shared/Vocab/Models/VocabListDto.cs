using GermanVocabApp.DataAccess.Shared.Core;

namespace GermanVocabApp.DataAccess.Shared.Vocab.Models;
public class VocabListDto : EntityDtoBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual IEnumerable<VocabListItemDto> ListItems { get; set; }
}
