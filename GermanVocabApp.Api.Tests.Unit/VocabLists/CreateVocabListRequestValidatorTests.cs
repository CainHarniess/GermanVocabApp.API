using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabLists;

namespace GermanVocabApp.Api.Tests.Unit.VocabLists;

public class CreateVocabListRequestValidatorTests : AbstractVocabListRequestValidatorTests<CreateListRequestValidator, CreateVocabListRequest>
{
    protected override CreateVocabListRequest CreateRequest() => new CreateVocabListRequest();

    protected override CreateListRequestValidator CreateValidator() => new CreateListRequestValidator();
}