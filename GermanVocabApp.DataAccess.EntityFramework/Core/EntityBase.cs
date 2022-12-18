namespace GermanVocabApp.DataAccess.EntityFramework.Core;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}
