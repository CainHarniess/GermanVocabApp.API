using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public abstract class AbstractAdjectiveRequestValidatorTests<TAdjectiveValidator, TAdjectiveRequest>
    : AbstractModifierRequestValidatorTests<TAdjectiveValidator, TAdjectiveRequest>
    where TAdjectiveValidator : AbstractListItemRequestValidator<TAdjectiveRequest>
    where TAdjectiveRequest : IListItemRequest
{
    protected AbstractAdjectiveRequestValidatorTests()
    {
        Request.WordType = WordType.Adjective;
    }
}
