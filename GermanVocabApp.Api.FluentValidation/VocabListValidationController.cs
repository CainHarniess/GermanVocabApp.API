using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Validation;

// TODO: Write unit tests.
public class VocabListValidationController<TItem> : IValidationController<IListRequest<TItem>>
    where TItem : IListItemRequest
{
    private readonly IValidator<IListRequest<TItem>> _listValidator;
    private readonly IFactory<FluentWordValidator, IListItemRequest> _wordValidatorFactory;

    public VocabListValidationController(IValidator<IListRequest<TItem>> listValidator, IFactory<FluentWordValidator, IListItemRequest> wordValidatorFactory)
    {
        _listValidator = listValidator;
        _wordValidatorFactory = wordValidatorFactory;
    }

    public ValidationResult Validate(IListRequest<TItem> target)
    {
        ValidationResult result = _listValidator.Validate(target);

        if (target.ListItems == null || !target.ListItems.Any())
        {
            return result;
        }

        TItem[] items = target.ListItems.ToArray();
        List<ValidationFailure> itemErrors = ValidateItems(items);

        if (!itemErrors.Any())
        {
            return result;
        }
        result.Errors.AddRange(itemErrors);
        return result;
    }

    private List<ValidationFailure> ValidateItems(TItem[] items)
    {
        List<ValidationFailure> itemErrors = new List<ValidationFailure>(items.Length);

        for (int i = 0; i < items.Length; i++)
        {
            TItem item = items[i];

            FluentWordValidator validator = _wordValidatorFactory.Create(item);
            ValidationResult itemResult = validator.Validate(item);

            if (itemResult.IsValid)
            {
                continue;
            }

            itemErrors.AddRange(itemResult.Errors);
        }

        return itemErrors;
    }
}
