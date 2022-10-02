namespace GermanVocabApp.Api.VocabLists.Models;

public class ListInfoResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string AuthorName { get; set; } = "Cain Harniess";
}
