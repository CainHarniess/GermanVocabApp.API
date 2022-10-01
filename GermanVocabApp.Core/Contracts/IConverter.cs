namespace GermanVocabApp.Core.Contracts;

public interface IConverter<TSource, TTarget>
{
    TTarget Convert(TSource source);
}
