using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabLists;

namespace GermanVocabApp.Api.Tests.Unit.VocabLists;

public class CreateVocabListRequestValidatorTests : AbstractVocabListRequestValidatorTests<CreateVocabListRequestValidator, CreateVocabListRequest>
{
    protected override CreateVocabListRequest CreateRequest() => new CreateVocabListRequest();

    protected override CreateVocabListRequestValidator CreateValidator() => new CreateVocabListRequestValidator();
}