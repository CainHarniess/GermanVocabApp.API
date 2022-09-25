using FluentValidation;
using GermanVocabApp.Api.VocabLists.Contracts;

namespace GermanVocabApp.Api.FluentValidation.Words;

public class ListValidator : AbstractValidator<IListRequest>
{
    public ListValidator() : base()
    {
        RuleFor(l => l.Name).NotNull().MinimumLength(3).MaximumLength(100);
        RuleFor(l => l.Description).MinimumLength(3).MaximumLength(100);
    }
}