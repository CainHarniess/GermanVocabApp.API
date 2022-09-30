using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class AggregateListItemValidator<TItem> : IAggregateValidator<TItem>
{
    private readonly IFactory<IValidator<TItem>, TItem> _validatorFactory;

    public AggregateListItemValidator(IFactory<IValidator<TItem>, TItem> validatorFactory)
    {
        _validatorFactory = validatorFactory;
    }

    public ValidationFailure[] Validate(IList<TItem> items)
    {
        List<ValidationFailure> itemErrors = new List<ValidationFailure>(items.Count);

        for (int i = 0; i < items.Count; i++)
        {
            TItem item = items[i];

            IValidator<TItem> validator = _validatorFactory.Create(item);
            ValidationResult itemResult = validator.Validate(item);

            if (itemResult.IsValid)
            {
                continue;
            }

            itemErrors.AddRange(itemResult.Errors);
        }

        return itemErrors.ToArray();
    }
}
