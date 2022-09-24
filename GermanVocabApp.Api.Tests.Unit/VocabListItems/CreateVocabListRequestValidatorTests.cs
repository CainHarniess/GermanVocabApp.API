using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public abstract class CreateListItemRequestValidatorTests<T>
    where T : CreateVocabListItemRequestValidator
{
    protected T _validator;
    protected CreateVocabListItemRequest _request;

    protected CreateListItemRequestValidatorTests()
    {
        _validator = Create();
        _request = new();
    }

    abstract protected T Create();

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
