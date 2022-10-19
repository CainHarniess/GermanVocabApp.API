namespace GermanVocabApp.DataAccess.Shared;

public interface IVocabUnitOfWorkAsync : IUnitOfWorkAsync
{
    IVocabListRepositoryAsync ListRepository { get; }
    IVocabListRepositoryAsync Items { get; }
}