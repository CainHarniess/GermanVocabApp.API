using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;

namespace GermanVocabApp.Api.FluentValidation.Validators;

public class FluentListValidator : AbstractValidator<ListRequest>
{
    public FluentListValidator() : base()
    {
        RuleFor(l => l.Name).NotNull().MinimumLength(3).MaximumLength(100);
        RuleFor(l => l.Description).MinimumLength(3).MaximumLength(100);
    }
}