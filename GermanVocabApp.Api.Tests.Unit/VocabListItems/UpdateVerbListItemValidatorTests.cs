using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class UpdateVerbListItemValidatorTests
    : AbstractVerbListItemRequestValidatorTests<UpdateVerbRequestValidator, UpdateVocabListItemRequest>
{
    protected override UpdateVerbRequestValidator CreateValidator()
    {
        return new UpdateVerbRequestValidator();
    }

    protected override UpdateVocabListItemRequest CreateRequest()
    {
        return new UpdateVocabListItemRequest()
        {
            WordType = WordType.Verb,
        };
    }
}
