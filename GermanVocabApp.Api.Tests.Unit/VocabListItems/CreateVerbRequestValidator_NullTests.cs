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

    #region IsWeakMasculineNoun
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsWeakMasculineNoun_ShouldHaveValidationError_WhenNotNull(bool value)
    {
        _request.IsWeakMasculineNoun = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }

    [Fact]
    public void IsWeakMasculineNoun_ShouldNotHaveValidationError_WhenNull()
    {
        _request.IsWeakMasculineNoun = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }
    #endregion

    #region ReflexiveCase
    [Theory]
    [InlineData(ReflexiveCase.Accusative)]
    [InlineData(ReflexiveCase.Dative)]
    [InlineData(null)]
    public void ReflexiveCase_ShouldNotHaveValidationError_WhenNull_OrValidValue(ReflexiveCase? value)
    {
        _request.ReflexiveCase = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.ReflexiveCase);
    }
    #endregion

    #region ThirdPersonPresent
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData("ab")]
    [InlineData(StringData.CharString26)]
    public void ThirdPersonPresent_ShouldHaveValidationError_WhenInvalidLength(string? value)
    {
        _request.ThirdPersonPresent = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void ThirdPersonPresent_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        _request.ThirdPersonPresent = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }
    #endregion

    #region ThirdPersonImperfect
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData("ab")]
    [InlineData(StringData.CharString26)]
    public void ThirdPersonImperfect_ShouldHaveValidationError_WhenInvalidLength(string? value)
    {
        _request.ThirdPersonImperfect = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void ThirdPersonImperfect_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        _request.ThirdPersonImperfect = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }
    #endregion

    #region AuxiliaryVerb
    [Theory]
    [InlineData(AuxiliaryVerb.Haben)]
    [InlineData(AuxiliaryVerb.Sein)]
    public void AuxiliaryVerb_ShouldNotHaveValidationError_WhenNotNull_AndValidEnum(AuxiliaryVerb value)
    {
        _request.AuxiliaryVerb = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Fact]
    public void AuxiliaryVerb_ShouldHaveValidationError_WhenNull()
    {
        _request.AuxiliaryVerb = null;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.AuxiliaryVerb);
    } 

    [Fact]
    public void AuxiliaryVerb_ShouldHaveValidationError_InvalidValue()
    {
        _request.AuxiliaryVerb = (AuxiliaryVerb)99;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }
    #endregion

    #region Perfect
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData("ab")]
    [InlineData(StringData.CharString26)]
    public void Perfect_ShouldHaveValidationError_WhenInvalidLength(string? value)
    {
        _request.Perfect = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Perfect);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Perfect_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        _request.Perfect = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Perfect);
    }
    #endregion

    #region Separability
    [Theory]
    [InlineData(Separability.None)]
    [InlineData(Separability.Separable)]
    [InlineData(Separability.Inseparable)]
    public void Separability_ShouldNotHaveValidationError_WhenNotNull(Separability value)
    {
        _request.Separability = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Separability);
    }

    [Fact]
    public void Separability_ShouldHaveValidationError_WhenNull()
    {
        _request.Separability = null;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Separability);
    }
    #endregion

    #region Transitivity
    [Theory]
    [InlineData(Transitivity.Transitive)]
    [InlineData(Transitivity.Intransitive)]
    [InlineData(Transitivity.Both)]
    public void Transitivity_ShouldNotHaveValidationError_WhenNotNull(Transitivity value)
    {
        _request.Transitivity = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Transitivity);
    }

    [Fact]
    public void Transitivity_ShouldHaveValidationError_WhenNull()
    {
        _request.Transitivity = null;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Transitivity);
    }
    #endregion

    #region Gender
    [Theory]
    [InlineData(Gender.Masculine)]
    [InlineData(Gender.Feminine)]
    [InlineData(Gender.Neuter)]
    public void Gender_ShouldHaveValidationError_WhenNotNull(Gender value)
    {
        _request.Gender = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Gender);
    }

    [Fact]
    public void Gender_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Gender = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Gender);
    }
    #endregion

    #region Plural
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.Whitespace)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Plural_ShouldHaveValidationError_WhenNotNull(string? value)
    {
        _request.Plural = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Plural);
    }

    [Fact]
    public void Plural_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Plural = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Plural);
    }
    #endregion

    #region Preposition
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData(StringData.CharString26)]
    public void Preposition_ShouldHaveValidationError_WhenInvalidLength(string? value)
    {
        _request.Preposition = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Preposition);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Preposition_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        _request.Preposition = value;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Preposition);
    }
    #endregion

    #region PrepositionCase
    
    [Theory(Skip = "Not yet implemented")]
    [InlineData(null)]
    public void PrepositionCase_ShouldNotHaveValidationError_WhenNull_AndPrepositionCaseNull(string? value)
    {

    }

    [Theory (Skip = "Not yet implemented")]
    [InlineData(null)]
    public void PrepositionCase_ShouldHaveValidationError_WhenNull_AndPrepositionNotNull(string? value)
    {

    }

    #endregion

    #region Modifiers
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.Whitespace)]
    [InlineData(StringData.CharString25)]
    public void Comparative_ShouldHaveValidationError_WhenNotNull(string? value)
    {
        _request.Comparative = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Comparative);
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
    [InlineData(StringData.Whitespace)]
    [InlineData(StringData.CharString25)]
    public void Superlative_ShouldHaveValidationError_WhenNotNull(string? value)
    {
        _request.Superlative = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.Superlative);
    }

    [Fact]
    public void Superlative_ShouldNotHaveValidationError_WhenNull()
    {
        _request.Superlative = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.Superlative);
    }
    #endregion

    #region FixedPlurality
    [Theory]
    [InlineData(FixedPlurality.None)]
    [InlineData(FixedPlurality.Singular)]
    [InlineData(FixedPlurality.Plural)]
    public void FixedPlurality_ShouldHaveValidationError_WhenNotNull(FixedPlurality value)
    {
        _request.FixedPlurality = value;
        var result = _validator.TestValidate(_request);
        result.ShouldHaveValidationErrorFor(request => request.FixedPlurality);
    }

    [Fact]
    public void FixedPlurality_ShouldNotHaveValidationError_WhenNull()
    {
        _request.FixedPlurality = null;
        var result = _validator.TestValidate(_request);
        result.ShouldNotHaveValidationErrorFor(request => request.FixedPlurality);
    } 
    #endregion
}