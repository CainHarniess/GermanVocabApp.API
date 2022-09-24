namespace GermanVocabApp.Core.Validation.DependencyInjection;

public interface IValidator<T>
{
    public abstract IValidationResult Validate(T target);
}
