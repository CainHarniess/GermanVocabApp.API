namespace GermanVocabApp.Api.VocabLists.Models;

public class ListResponse : ListInfoResponse
{
    public IEnumerable<ItemResponse> ListItems { get; set; }
}