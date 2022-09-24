using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class CreateNounRequestValidatorTests : AbstractNounRequestValidatorTests<CreateNounRequestValidator, CreateVocabListItemRequest>
{
    protected override CreateNounRequestValidator CreateValidator()
    {
        return new CreateNounRequestValidator();
    }

    protected override CreateVocabListItemRequest CreateRequest()
    {
        return new CreateVocabListItemRequest();
    }
}
