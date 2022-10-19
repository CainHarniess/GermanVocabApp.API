namespace GermanVocabApp.DataAccess.Shared;
public interface IUnitOfWorkAsync
{
    Task CommitAsync();
}