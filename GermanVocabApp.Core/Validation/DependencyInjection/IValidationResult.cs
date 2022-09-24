namespace GermanVocabApp.Core.Validation.DependencyInjection;

public interface IValidationResult
{
    bool IsValid { get; }

    List<ValidationError>? Errors { get; }
}
