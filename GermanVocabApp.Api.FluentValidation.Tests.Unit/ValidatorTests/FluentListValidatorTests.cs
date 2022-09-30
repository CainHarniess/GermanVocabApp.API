using FluentValidation.TestHelper;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.ValidatorTests;

public abstract class FluentListValidatorTests
{
    private readonly Mock<ListRequest> _mock;
    private readonly FluentListValidator _validator;

    public FluentListValidatorTests()
    {
        _mock = new Mock<ListRequest>();
        _validator = new FluentListValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdea")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeab")]
    public void Name_ShouldHaveErrorWhenOutsideValidLengthRange(string value)
    {
        _mock.Setup(r => r.Name).Returns(value);
        var result = _validator.TestValidate(_mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.Name);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("abcd")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcd")]
    [InlineData("abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde")]
    public void Name_ShouldNotHaveErrorWhenInsideValidLengthRange(string value)
    {
        _mock.Setup(r => r.Name).Returns(value);
        var result = _validator.TestValidate(_mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.Name);
    }
}
