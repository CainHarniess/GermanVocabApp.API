using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Api.VocabLists.Validation.VocabLists;

namespace GermanVocabApp.Api.Tests.Unit.VocabLists;

public abstract class AbstractVocabListRequestValidatorTests<TValidator, TRequest>
    where TValidator : AbstractListRequestValidator<TRequest>
    where TRequest : IVocabListRequest
{
    private readonly TValidator _validator;
    private readonly TRequest _request;

    protected AbstractVocabListRequestValidatorTests()
    {
        _validator = CreateValidator();
        _request = CreateRequest();
    }

    protected TValidator Validator => _validator;
    protected TRequest Request => _request;

    abstract protected TValidator CreateValidator();
    abstract protected TRequest CreateRequest();

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdea")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeab")]
    public void Name_ShouldHaveErrorWhenOutsideValidLengthRange(string value)
    {
        _request.Name = value;
        var result = _validator.TestValidate(_request);
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
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Name);
    }
}
