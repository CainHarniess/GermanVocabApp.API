using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class VocabListValidationController : IValidationController<ListRequest>
{
    private readonly IValidator<ListRequest> _listValidator;
    private readonly IAggregateValidator<ItemRequest> _listItemValidator;

    public VocabListValidationController(IValidator<ListRequest> listValidator, IAggregateValidator<ItemRequest> listItemValidator)
    {
        _listValidator = listValidator;
        _listItemValidator = listItemValidator;
    }

    public ValidationResult Validate(ListRequest target)
    {
        ValidationResult result = _listValidator.Validate(target);

        if (target.ListItems == null || !target.ListItems.Any())
        {
            return result;
        }

        ItemRequest[] items = target.ListItems.ToArray();
        ValidationFailure[] itemErrors = _listItemValidator.Validate(items);

        if (!itemErrors.Any())
        {
            return result;
        }
        result.Errors.AddRange(itemErrors);
        return result;
    }
}
