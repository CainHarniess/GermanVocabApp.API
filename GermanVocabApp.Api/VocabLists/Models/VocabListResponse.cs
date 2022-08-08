namespace GermanVocabApp.Api.VocabLists.Models;

internal class VocabListResponse : VocabListInfoResponse
{
    public IEnumerable<VocabListItemResponse> ListItems { get; set; }
}

