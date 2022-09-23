using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class CreateNounRequestValidator_NullTests : CreateListItemRequestValidatorTests<CreateNounRequestValidator>
{
    public CreateNounRequestValidator_NullTests() : base()
    {
        _request.WordType = WordType.Noun;
    }

    protected override CreateNounRequestValidator Create()
    {
        return new CreateNounRequestValidator();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsWeakMasculineNoun_ShouldHaveValidationError_WhenNotNull(bool value)
    {
        _request.IsWeakMasculineNoun = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }

    [Theory]
    [InlineData(null)]
    public void IsWeakMasculineNoun_ShouldHaveValidationError_WhenNull(bool? value)
    {
        _request.IsWeakMasculineNoun = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }

    [Fact]
    public void ReflexiveCase_ShouldNotHaveValidationError_WhenNull()
    {
        _request.ReflexiveCase = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.ReflexiveCase);
    }

    [Theory]
    [InlineData(ReflexiveCase.Accusative)]
    [InlineData(ReflexiveCase.Dative)]
    public void ReflexiveCase_ShouldHaveValidationError_WhenNotNull(ReflexiveCase value)
    {
        _request.ReflexiveCase = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.ReflexiveCase);
    }

    [Fact]
    public void Separability_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Separability = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Separability);
    }

    [Theory]
    [InlineData(Separability.None)]
    [InlineData(Separability.Separable)]
    [InlineData(Separability.Inseparable)]
    public void Separability_ShouldHaveValidationError_WhenNotNull(Separability value)
    {
        _request.Separability = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Separability);
    }

    [Fact]
    public void Transitivity_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Transitivity = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Transitivity);
    }

    [Theory]
    [InlineData(Transitivity.Transitive)]
    [InlineData(Transitivity.Intransitive)]
    [InlineData(Transitivity.Both)]
    public void Transitivity_ShouldHaveValidationError_WhenNotNull(Transitivity value)
    {
        _request.Transitivity = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Transitivity);
    }

    [Fact]
    public void ThirdPersonPresent_ShouldNotHaveValidationError_WhenNull()
    {
        _request.ThirdPersonPresent = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void ThirdPersonPresent_ShouldHaveValidationError_WhenNotNull(string value)
    {
        _request.ThirdPersonPresent = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }

    [Fact]
    public void ThirdPersonImperfect_ShouldNotHaveValidationError_WhenNull()
    {
        _request.ThirdPersonImperfect = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void ThirdPersonImperfect_ShouldHaveValidationError_WhenNotNull(string value)
    {
        _request.ThirdPersonImperfect = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }

    [Fact]
    public void AuxiliaryVerb_ShouldNotHaveValidationError_WhenNull()
    {
        _request.AuxiliaryVerb = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Theory]
    [InlineData(AuxiliaryVerb.Haben)]
    [InlineData(AuxiliaryVerb.Sein)]
    public void AuxiliaryVerb_ShouldHaveValidationError_WhenNotNull(AuxiliaryVerb value)
    {
        _request.AuxiliaryVerb = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Fact]
    public void Perfect_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Perfect = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Perfect);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void Perfect_ShouldHaveValidationError_WhenNotNull(string value)
    {
        _request.Perfect = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Perfect);
    }

    [Fact]
    public void Gender_ShouldHaveValidationError_WhenNull()
    {
        _request.Gender = null;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Gender);
    }

    [Theory]
    [InlineData(Gender.Masculine)]
    [InlineData(Gender.Feminine)]
    [InlineData(Gender.Neuter)]
    public void Gender_ShouldNotHaveValidationError_WhenNotNull(Gender value)
    {
        _request.Gender = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Gender);
    }

    [Fact]
    public void FixedPlurality_ShouldHaveValidationError_WhenNull()
    {
        _request.FixedPlurality = null;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.FixedPlurality);
    }

    [Fact]
    public void Comparative_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Comparative = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Comparative);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void Comparative_ShouldHaveValidationError_WhenNotNull(string value)
    {
        _request.Comparative = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Comparative);
    }

    [Fact]
    public void Superlative_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Superlative = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Superlative);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void Superlative_ShouldHaveValidationError_WhenNotNull(string value)
    {
        _request.Superlative = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Superlative);
    }

    [Theory]
    [InlineData(FixedPlurality.None)]
    [InlineData(FixedPlurality.Singular)]
    [InlineData(FixedPlurality.Plural)]
    public void FixedPlurality_ShouldNotHaveValidationError_WhenNotNull(FixedPlurality value)
    {
        _request.FixedPlurality = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.FixedPlurality);
    }
}
