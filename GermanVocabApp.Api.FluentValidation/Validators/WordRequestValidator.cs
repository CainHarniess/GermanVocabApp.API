using FluentValidation;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.FluentValidation.Validators;

internal abstract class FluentWordValidator : AbstractValidator<IListItemRequest>
{
    protected FluentWordValidator()
    {
        RuleFor(w => w.WordType).NotNull();
        RuleFor(w => w.German).NotNull().MinimumLength(3).MaximumLength(100);
        RuleFor(w => w.English).NotNull().MinimumLength(3).MaximumLength(100);
    }
}
