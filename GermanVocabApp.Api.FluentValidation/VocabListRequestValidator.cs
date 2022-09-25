using FluentValidation.Results;
using GermanVocabApp.Api.FluentValidation;
using GermanVocabApp.Api.FluentValidation.FluentValidators;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class VocabListRequestValidator : IValidator<IListRequest>
{
    private readonly FluentListValidator _listValidator;
    private readonly WordValidatorFactory _wordValidatorFactory;

    public VocabListRequestValidator()
    {
        _listValidator = new FluentListValidator();
        _wordValidatorFactory = new WordValidatorFactory();
    }

    internal VocabListRequestValidator(FluentListValidator listValidator, WordValidatorFactory wordValidatorFactory)
    {
        _listValidator = listValidator;
        _wordValidatorFactory = wordValidatorFactory;
    }

    public ValidationResult Validate(IListRequest target)
    {
        ValidationResult result = _listValidator.Validate(target);

        if (target.ListItems == null || target.ListItems.Any())
        {
            return result;
        }

        IListItemRequest[] items = target.ListItems.ToArray();
        List<ValidationFailure> itemErrors = ValidateItems(items);

        if (!itemErrors.Any())
        {
            return result;
        }
        result.Errors.AddRange(itemErrors);
        return result;
    }

    private List<ValidationFailure> ValidateItems(IListItemRequest[] items)
    {
        List<ValidationFailure> itemErrors = new List<ValidationFailure>(items.Length);

        for (int i = 0; i < items.Length; i++)
        {
            IListItemRequest item = items[i];

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
