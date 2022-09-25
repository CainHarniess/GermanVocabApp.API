using FluentValidation;
using GermanVocabApp.Api.VocabLists.Contracts;

namespace GermanVocabApp.Api.FluentValidation.FluentValidators;

internal abstract class FluentWordValidator : AbstractValidator<IListItemRequest>
{
    protected FluentWordValidator()
    {
        RuleFor(w => w.WordType).NotNull();
        RuleFor(w => w.German).NotNull().MinimumLength(3).MaximumLength(100);
        RuleFor(w => w.English).NotNull().MinimumLength(3).MaximumLength(100);
    }
}
