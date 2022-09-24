using GermanVocabApp.Core.Validation.DependencyInjection;

namespace GermanVocabApp.Core.Validation;

public class ValidationResult : IValidationResult
{
    public bool IsValid { get; private set; }

    public List<ValidationError> Errors { get; private set; }

    public ValidationResult(bool isValid)
    {
        IsValid = isValid;
        Errors = new List<ValidationError>(5);
    }
}
