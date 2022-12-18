using GermanVocabApp.DataAccess.Shared.Core;

namespace GermanVocabApp.DataAccess.Shared.Vocab.Models;

public class VocabListInfoDto : QueryDtoBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
