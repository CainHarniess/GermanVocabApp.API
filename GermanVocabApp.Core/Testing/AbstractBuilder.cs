namespace GermanVocabApp.Core.Testing;
public abstract class AbstractBuilder<T>
    where T : class
{
    public abstract T Build();
}
