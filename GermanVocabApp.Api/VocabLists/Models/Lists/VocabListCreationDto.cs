using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Models;

public class VocabListCreationDto : IVocabList
{
    public string Name { get; set; }
    public string Description {get; set;}
    public IEnumerable<VocabListItemCreationDto>? ListItems { get; set; }
}
