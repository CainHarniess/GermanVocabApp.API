using FluentValidation;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Shared.Validation;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

public abstract class AbstractListItemRequestValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : IListItemRequest
{
    protected AbstractListItemRequestValidator()
    {
        RuleFor(w => w.German).NotNullEmptyNorWhiteSpace();
        RuleFor(w => w.German).StringLengthRange(ListItemValidationData.EnglishMinLength, ListItemValidationData.EnglishMaxLength);

        RuleFor(w => w.English).NotNullEmptyNorWhiteSpace();
        RuleFor(w => w.English).StringLengthRange(ListItemValidationData.GermanMinLength, ListItemValidationData.GermanMaxLength);
    }
}
