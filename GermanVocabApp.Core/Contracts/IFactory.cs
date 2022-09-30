namespace GermanVocabApp.Core.Contracts;

public interface IFactory<T>
{
    public T Create();
}

public interface IFactory<TResult, TSource1>
{
    public TResult Create(TSource1 source);
}