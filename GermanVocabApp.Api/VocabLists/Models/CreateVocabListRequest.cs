using GermanVocabApp.Api.VocabLists.Contracts;

namespace GermanVocabApp.Api.VocabLists.Models;

public class CreateVocabListRequest : IVocabListRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<CreateVocabListItemRequest> ListItems { get; set; }
}