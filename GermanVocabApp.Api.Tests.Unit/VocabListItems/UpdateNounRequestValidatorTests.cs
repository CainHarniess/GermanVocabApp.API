using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class UpdateNounRequestValidatorTests : AbstractNounRequestValidatorTests<UpdateNounRequestValidator, UpdateVocabListItemRequest>
{
    protected override UpdateNounRequestValidator CreateValidator()
    {
        return new UpdateNounRequestValidator();
    }

    protected override UpdateVocabListItemRequest CreateRequest()
    {
        return new UpdateVocabListItemRequest();
    }
}
