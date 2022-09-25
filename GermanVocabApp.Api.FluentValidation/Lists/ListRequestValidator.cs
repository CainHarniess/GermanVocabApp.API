using FluentValidation;
using GermanVocabApp.Api.VocabLists.Contracts;

namespace GermanVocabApp.Api.FluentValidation.Lists;

public class ListRequestValidator : AbstractValidator<IListRequest>
{
    public ListRequestValidator() : base()
    {
        RuleFor(l => l.Name).NotNull().MinimumLength(3).MaximumLength(100);
        RuleFor(l => l.Description).MinimumLength(3).MaximumLength(100);
    }
}