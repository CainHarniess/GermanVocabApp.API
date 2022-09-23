using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabLists;

namespace GermanVocabApp.Api.Tests.Unit.VocabLists;

public class CreateVocabListRequestValidatorTests
{
    private CreateVocabListRequestValidator _validator;
    private CreateVocabListRequest _request;

    public CreateVocabListRequestValidatorTests()
    {
        _validator = new();
        _request = new CreateVocabListRequest();
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdea")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeab")]
    public void Name_ShouldHaveErrorWhenOutsideValidLengthRange(string value)
    {
        _request.Name = value;
        TestValidationResult<CreateVocabListRequest> result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Name);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("abcd")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcd")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde")]
    public void Name_ShouldNotHaveErrorWhenInsideValidLengthRange(string value)
    {
        _request.Name = value;
        TestValidationResult<CreateVocabListRequest> result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Name);
    }
}