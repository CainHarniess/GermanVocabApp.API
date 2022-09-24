using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabLists;

namespace GermanVocabApp.Api.Tests.Unit.VocabLists;

public class UpdateVocabListRequestValidatorTests : AbstractVocabListRequestValidatorTests<UpdateListRequestValidator, UpdateVocabListRequest>
{
    protected override UpdateVocabListRequest CreateRequest()
    {
        return new UpdateVocabListRequest();
    }

    protected override UpdateListRequestValidator CreateValidator()
    {
        return new UpdateListRequestValidator();
    }
}