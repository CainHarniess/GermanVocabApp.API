using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class CreateAdverbRequestValidatorTests : AbstractAdverbRequestValidatorTests<CreateAdverbRequestValidator, CreateVocabListItemRequest>
{
    protected override CreateVocabListItemRequest CreateRequest()
    {
        return new CreateVocabListItemRequest();
    }

    protected override CreateAdverbRequestValidator CreateValidator()
    {
        return new CreateAdverbRequestValidator();
    }
}

