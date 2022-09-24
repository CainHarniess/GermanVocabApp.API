using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class UpdateAdverbRequestValidatorTests : AbstractAdverbRequestValidatorTests<UpdateAdverbRequestValidator, CreateVocabListItemRequest>
{
    protected override CreateVocabListItemRequest CreateRequest()
    {
        return new CreateVocabListItemRequest()
        {
            WordType = WordType.Adverb,
        };
    }

    protected override UpdateAdverbRequestValidator CreateValidator()
    {
        return new UpdateAdverbRequestValidator();
    }
}

