using GermanVocabApp.Api.VocabLists.Contracts;

namespace GermanVocabApp.Api.VocabLists.Models;

public class UpdateVocabListRequest : IListRequest<UpdateVocabListItemRequest>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<UpdateVocabListItemRequest> ListItems { get; set; }
}
