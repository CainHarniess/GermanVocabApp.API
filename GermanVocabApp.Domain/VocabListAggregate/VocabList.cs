using GermanVocabApp.Domain.Core;

namespace GermanVocabApp.Domain.VocabListAggregate;

public class VocabList : Entity, IVocabList
{
    public string Name { get; set; }

    public string Description { get; set; }

    public List<VocabListItem>? ListItems { get; set; }
}
