using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Models;

public class FullVocabListDto : IVocabList
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<VocabListItem> ListItems { get; set; }
}

