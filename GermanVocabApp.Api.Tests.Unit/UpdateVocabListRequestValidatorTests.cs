using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation;
namespace GermanVocabApp.Api.Tests.Unit;

public class UpdateVocabListRequestValidatorTests
{
    private UpdateVocabListRequestValidator _validator;
    private UpdateVocabListRequest _request;

    public UpdateVocabListRequestValidatorTests()
    {
        _validator = new();
        _request = new UpdateVocabListRequest();
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
        TestValidationResult<UpdateVocabListRequest> result = _validator.TestValidate(_request);
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
        TestValidationResult<UpdateVocabListRequest> result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Name);
    }
}