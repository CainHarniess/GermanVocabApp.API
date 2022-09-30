using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class AggregateListItemValidator : IAggregateValidator<ItemRequest>
{
    private readonly IFactory<IValidator<ItemRequest>, ItemRequest> _validatorFactory;

    public AggregateListItemValidator(IFactory<IValidator<ItemRequest>, ItemRequest> validatorFactory)
    {
        _validatorFactory = validatorFactory;
    }

    public ValidationFailure[] Validate(IList<ItemRequest> items)
    {
        List<ValidationFailure> itemErrors = new List<ValidationFailure>(items.Count);

        for (int i = 0; i < items.Count; i++)
        {
            ItemRequest item = items[i];

            IValidator<ItemRequest> validator = _validatorFactory.Create(item);
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
