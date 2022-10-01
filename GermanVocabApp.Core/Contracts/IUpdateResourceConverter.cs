namespace GermanVocabApp.Core.Contracts;
public interface IUpdateResourceConverter<TSource, TTarget>
{
    TTarget Convert(TSource source, Guid resourceId);
}