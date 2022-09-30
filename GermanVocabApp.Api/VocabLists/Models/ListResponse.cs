namespace GermanVocabApp.Api.VocabLists.Models;

internal class ListResponse : ListInfoResponse
{
    public IEnumerable<ItemResponse> ListItems { get; set; }
}