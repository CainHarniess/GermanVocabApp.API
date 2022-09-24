using GermanVocabApp.Core.Validation.DependencyInjection;

namespace GermanVocabApp.Core.Validation;

public abstract class Validator<T> : IValidator<T>
{
    public abstract IValidationResult Validate(T target);
}
