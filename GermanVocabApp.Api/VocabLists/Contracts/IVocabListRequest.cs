using GermanVocabApp.Api.VocabLists.Models;

namespace GermanVocabApp.Api.VocabLists.Contracts;

public interface IVocabListRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
