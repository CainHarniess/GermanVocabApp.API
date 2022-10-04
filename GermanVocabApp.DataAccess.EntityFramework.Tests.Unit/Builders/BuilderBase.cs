namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;

public abstract class BuilderBase<T> where T : class
{
    public abstract T Build();
}
