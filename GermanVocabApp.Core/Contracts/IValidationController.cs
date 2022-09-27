using FluentValidation.Results;

namespace GermanVocabApp.Core.Contracts;

public interface IValidationController<T>
{
    public ValidationResult Validate(T target);
}