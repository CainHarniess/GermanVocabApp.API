using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class VocabListValidationController<TItem> : IValidationController<IListRequest<TItem>>
    where TItem : IListItemRequest
{
    private readonly IValidator<IListRequest<TItem>> _listValidator;
    private readonly IAggregateValidator<TItem> _listItemValidator;

    public VocabListValidationController(IValidator<IListRequest<TItem>> listValidator, IAggregateValidator<TItem> listItemValidator)
    {
        _listValidator = listValidator;
        _listItemValidator = listItemValidator;
    }

    public ValidationResult Validate(IListRequest<TItem> target)
    {
        ValidationResult result = _listValidator.Validate(target);

        if (target.ListItems == null || !target.ListItems.Any())
        {
            return result;
        }

        TItem[] items = target.ListItems.ToArray();
        ValidationFailure[] itemErrors = _listItemValidator.Validate(items);

        if (!itemErrors.Any())
        {
            return result;
        }
        result.Errors.AddRange(itemErrors);
        return result;
    }
}
