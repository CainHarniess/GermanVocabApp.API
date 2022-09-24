using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public abstract class AbstractListItemRequestValidatorTests<TValidator, TRequest>
    where TValidator : AbstractListItemRequestValidator<TRequest>
    where TRequest : IListItemRequest
{
    private readonly TValidator _validator;
    private readonly TRequest _request;

    protected AbstractListItemRequestValidatorTests()
    {
        _validator = CreateValidator();
        _request = CreateRequest();
    }

    abstract protected TValidator CreateValidator();

    abstract protected TRequest CreateRequest();

    protected TValidator Validator => _validator;
    protected TRequest Request => _request;

    #region German
    [Theory]
    [InlineData(null)]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData("ab")]
    [InlineData(StringData.CharString101)]
    public void German_ShouldHaveError_WhenNull_OrOutsideValidLengthRange(string? value)
    {
        _request.German = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.German);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("abcd")]
    [InlineData(StringData.CharString99)]
    [InlineData(StringData.CharString100)]
    public void German_ShouldNotHaveError_WhenInsideValidLengthRange(string? value)
    {
        _request.German = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.German);
    }
    #endregion

    #region English
    [Theory]
    [InlineData(null)]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData("ab")]
    [InlineData(StringData.CharString101)]
    public void English_ShouldHaveError_WhenNull_OrOutsideValidLengthRange(string? value)
    {
        _request.English = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.English);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("abcd")]
    [InlineData(StringData.CharString99)]
    [InlineData(StringData.CharString100)]
    public void English_ShouldNotHaveError_WhenInsideValidLengthRange(string? value)
    {
        _request.English = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.English);
    }
    #endregion
}
