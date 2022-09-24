using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Shared.Validation;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

public abstract class CreateVocabListItemRequestValidator : AbstractValidator<CreateVocabListItemRequest>
{
    protected CreateVocabListItemRequestValidator()
    {
        RuleFor(w => w.German).NotNullEmptyNorWhiteSpace();
        RuleFor(w => w.German).StringLengthRange(ListItemValidationData.EnglishMinLength, ListItemValidationData.EnglishMaxLength);

        RuleFor(w => w.English).NotNullEmptyNorWhiteSpace();
        RuleFor(w => w.English).StringLengthRange(ListItemValidationData.GermanMinLength, ListItemValidationData.GermanMaxLength);
    }
}
