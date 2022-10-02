using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Models;

public class ListRequest : IListRequest<ItemRequest>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<ItemRequest> ListItems { get; set; }
}
