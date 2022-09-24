using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class UpdateAdjectiveRequestValidatorTests : AbstractAdjectiveRequestValidatorTests<UpdateAdjectiveRequestValidator, CreateVocabListItemRequest>
{
    protected override CreateVocabListItemRequest CreateRequest()
    {
        return new CreateVocabListItemRequest()
        {
            WordType = WordType.Adjective,
        };
    }

    protected override UpdateAdjectiveRequestValidator CreateValidator()
    {
        return new UpdateAdjectiveRequestValidator();
    }
}

