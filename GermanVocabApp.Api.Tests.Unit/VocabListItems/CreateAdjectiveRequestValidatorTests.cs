using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class CreateAdjectiveRequestValidatorTests : AbstractAdjectiveRequestValidatorTests<CreateAdjectiveRequestValidator, CreateVocabListItemRequest>
{
    protected override CreateVocabListItemRequest CreateRequest()
    {
        return new CreateVocabListItemRequest();
    }

    protected override CreateAdjectiveRequestValidator CreateValidator()
    {
        return new CreateAdjectiveRequestValidator();
    }
}

