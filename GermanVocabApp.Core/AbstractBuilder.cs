namespace GermanVocabApp.Core;

public abstract class AbstractBuilder<T>
{
    public abstract T Build();

    abstract protected void ApplyValues(T item);

    abstract protected void Clear();
}
