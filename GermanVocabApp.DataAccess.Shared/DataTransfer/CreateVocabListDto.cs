namespace GermanVocabApp.DataAccess.Shared.DataTransfer;

public class CreateVocabListDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<CreateVocabListItemDto> ListItems { get; set; }
}
