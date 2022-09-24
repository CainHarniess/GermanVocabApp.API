using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Validation;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class UpdateItemValidator : Validator<UpdateVocabListItemRequest>
{
    public override ValidationResult Validate(UpdateVocabListItemRequest target)
    {
        Console.WriteLine($"Validating list item {target.English}");
        return new ValidationResult(true);
    }
}