using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public abstract class AbstractAdverbRequestValidatorTests<TAdverbValidator, TAdverbRequest>
    : AbstractModifierRequestValidatorTests<TAdverbValidator, TAdverbRequest>
    where TAdverbValidator : AbstractListItemRequestValidator<TAdverbRequest>
    where TAdverbRequest : IListItemRequest
{
    public AbstractAdverbRequestValidatorTests()
    {
        Request.WordType = WordType.Adverb;
    }
}