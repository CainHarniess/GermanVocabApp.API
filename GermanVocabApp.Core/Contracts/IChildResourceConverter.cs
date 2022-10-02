namespace GermanVocabApp.Core.Contracts;

public interface IChildResourceConverter<TSource, TTarget>
{
    TTarget Convert(TSource source, Guid parentId);
}
