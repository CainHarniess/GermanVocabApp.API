using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class CreateVerbListItemValidatorTests
    : AbstractVerbListItemRequestValidatorTests<CreateVerbRequestValidator, CreateVocabListItemRequest>
{
    protected override CreateVerbRequestValidator CreateValidator()
    {
        return new CreateVerbRequestValidator();
    }

    protected override CreateVocabListItemRequest CreateRequest()
    {
        return new CreateVocabListItemRequest()
        {
            WordType = WordType.Verb,
        };
    }
}
