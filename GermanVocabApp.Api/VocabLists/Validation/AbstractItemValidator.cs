﻿using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Validation;
using GermanVocabApp.Core.Validation.DependencyInjection;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class AbstractItemValidator<TItem> : Validator<TItem>
    where TItem : IListItemRequest
{
    public override IValidationResult Validate(TItem target)
    {
        Console.WriteLine($"Validating list item {target.English}");
        return new ValidationResult(true);
    }
}
