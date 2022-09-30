using FluentValidation.Results;

namespace GermanVocabApp.Core.Contracts;

public interface IAggregateValidator<TItem>
{
    ValidationFailure[] Validate(IList<TItem> items);
}