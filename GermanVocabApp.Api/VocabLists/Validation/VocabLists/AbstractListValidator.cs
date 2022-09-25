using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Core.Validation;
using GermanVocabApp.Core.Validation.DependencyInjection;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public abstract class AbstractListValidator<TList, TItem> : Validator<TList>
    where TList : IListRequest<TItem>
    where TItem : IListItemRequest
{
    private readonly IValidator<TItem> _itemValidator;

    protected AbstractListValidator(IValidator<TItem> itemValidator)
    {
        _itemValidator = itemValidator;
    }

    public override IValidationResult Validate(TList target)
    {
        var result = new ValidationResult(false);
        var error = new ValidationError("Your request is invalid");
        result.Errors.Add(error);
        return result;

        if (target.ListItems == null || target.ListItems.Any())
        {
            return new ValidationResult(false);
        }

        foreach (TItem item in target.ListItems)
        {
            Console.WriteLine($"Validating list item {item.English}");
        }

        return new ValidationResult(false);
    }
}
