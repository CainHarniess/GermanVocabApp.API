using FluentValidation.Results;

namespace GermanVocabApp.Core.Contracts;

public interface IValidator<T>
{
    public ValidationResult Validate(T target);
}