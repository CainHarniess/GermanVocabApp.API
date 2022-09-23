using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Shared.Validation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

public abstract class CreateVocabListItemRequestValidator : AbstractValidator<CreateVocabListItemRequest>
{
    protected CreateVocabListItemRequestValidator()
    {
        RuleFor(w => w.German).MinimumLength(VocabListItemValidationData.EnglishMinLength)
                              .MaximumLength(VocabListItemValidationData.EnglishMaxLength);

        RuleFor(w => w.English).MinimumLength(VocabListItemValidationData.GermanMinLength)
                               .MaximumLength(VocabListItemValidationData.GermanMaxLength);
    }
}
