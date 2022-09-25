using FluentValidation;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.FluentValidation.FluentValidators;

internal class FluentListValidator<TItem> : AbstractValidator<IListRequest<TItem>>
    where TItem : IListItemRequest
{
    public FluentListValidator() : base()
    {
        RuleFor(l => l.Name).NotNull().MinimumLength(3).MaximumLength(100);
        RuleFor(l => l.Description).MinimumLength(3).MaximumLength(100);
    }
}