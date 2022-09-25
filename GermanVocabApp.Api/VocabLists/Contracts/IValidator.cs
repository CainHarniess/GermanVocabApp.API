using FluentValidation.Results;

namespace GermanVocabApp.Api.VocabLists.Contracts;

public interface IValidator<T>
{
    public ValidationResult Validate(T target);
}