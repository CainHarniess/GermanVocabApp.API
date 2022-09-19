namespace GermanVocabApp.DataAccess.Shared.Abstractions;
public interface ISoftDeletable : IEntity
{
    public DateTime? DeletedDate { get; set; }
}
