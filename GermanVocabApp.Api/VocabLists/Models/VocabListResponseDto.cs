using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.Models;

public class VocabListResponseDto : IVocabList
{
    public Guid Id { get; set; }
    public string Name {get; set;}

    public string Description {get; set;}
}
