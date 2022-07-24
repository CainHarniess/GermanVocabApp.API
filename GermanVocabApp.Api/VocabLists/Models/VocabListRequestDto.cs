using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Models;

public class VocabListRequestDto : IVocabList
{
    public string Name { get; set; }

    public string Description {get; set;}
}
