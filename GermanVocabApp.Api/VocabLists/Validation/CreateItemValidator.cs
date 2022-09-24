using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Validation;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class CreateItemValidator : Validator<CreateVocabListItemRequest>
{
    public override ValidationResult Validate(CreateVocabListItemRequest target)
    {
        Console.WriteLine($"Validating list item {target.English}");
        return new ValidationResult(true);
    }
}
