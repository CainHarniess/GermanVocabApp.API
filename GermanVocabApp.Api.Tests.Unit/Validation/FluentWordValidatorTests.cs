using FluentValidation.TestHelper;
using GermanVocabApp.Api.Validators;
using GermanVocabApp.Core.Contracts;
using Moq;

namespace GermanVocabApp.Api.Tests.Unit.Validation;

public abstract class FluentWordValidatorTests<TValidator>
    where TValidator : FluentWordValidator
{
    private readonly TValidator _validator;

    private Mock<IListItemRequest> _mock;

    protected FluentWordValidatorTests()
    {
        _mock = new Mock<IListItemRequest>();
        _validator = CreateValidator();
    }

    abstract protected TValidator CreateValidator();

    protected TValidator Validator => _validator;
    protected Mock<IListItemRequest> Mock => _mock;

    #region German
    [Theory]
    [InlineData(null)]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData("ab")]
    [InlineData(StringData.CharString101)]
    public void German_ShouldHaveError_WhenNull_OrOutsideValidLengthRange(string? value)
    {
        _mock.Setup(r => r.German).Returns(value);
        var result = _validator.TestValidate(_mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.German);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("abcd")]
    [InlineData(StringData.CharString99)]
    [InlineData(StringData.CharString100)]
    public void German_ShouldNotHaveError_WhenInsideValidLengthRange(string? value)
    {
        _mock.Setup(r => r.German).Returns(value);
        var result = _validator.TestValidate(_mock.Object);
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
        _mock.Setup(r => r.English).Returns(value);
        var result = _validator.TestValidate(_mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.English);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("abcd")]
    [InlineData(StringData.CharString99)]
    [InlineData(StringData.CharString100)]
    public void English_ShouldNotHaveError_WhenInsideValidLengthRange(string? value)
    {
        _mock.Setup(r => r.English).Returns(value);
        var result = _validator.TestValidate(_mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.English);
    }
    #endregion
}
