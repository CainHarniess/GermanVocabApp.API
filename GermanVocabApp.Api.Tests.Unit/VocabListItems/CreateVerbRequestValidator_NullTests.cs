using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class CreateVerbRequestValidator_NullTests : CreateListItemRequestValidatorTests<CreateVerbRequestValidator>
{
    public CreateVerbRequestValidator_NullTests() : base()
    {
        _request.WordType = WordType.Verb;
    }

    protected override CreateVerbRequestValidator Create()
    {
        return new CreateVerbRequestValidator();
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData("ab")]
    [InlineData(StringData.CharString26)]
    public void ThirdPersonPresent_ShouldHaveValidationError_WhenInvalidLength(string value)
    {
        _request.ThirdPersonPresent = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void ThirdPersonPresent_ShouldNotHaveValidationError_WhenNull_OrValidLength(string value)
    {
        _request.ThirdPersonPresent = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }
}