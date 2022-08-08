namespace GermanVocabApp.DataAccess.EntityFramework.DataTransfer;

public abstract class EntityDtoBase
{
    public Guid Id { get; set; }
}

public class VocabListInfoDto : EntityDtoBase
{
    public string Name { get; set; }
    public string Description { get; set; }
}
