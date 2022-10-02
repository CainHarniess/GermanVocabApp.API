namespace GermanVocabApp.DataAccess.Shared.DataTransfer;

public class VocabListInfoDto : QueryDtoBase
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
