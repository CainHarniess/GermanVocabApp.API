using FluentValidation.TestHelper;
using GermanVocabApp.Api.FluentValidation.Tests.Unit;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public class FluentVerbValidatorTests : FluentWordValidatorTests<FluentVerbValidator>
{
    protected override FluentVerbValidator CreateValidator()
    {
        return new FluentVerbValidator();
    }

    #region IsWeakMasculineNoun
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsWeakMasculineNoun_ShouldHaveValidationError_WhenNotNull(bool value)
    {
        Mock.Setup(r => r.IsWeakMasculineNoun).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }

    [Fact]
    public void IsWeakMasculineNoun_ShouldNotHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.IsWeakMasculineNoun).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.ReflexiveCase).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.ThirdPersonPresent).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void ThirdPersonPresent_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Mock.Setup(r => r.ThirdPersonPresent).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.ThirdPersonImperfect).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void ThirdPersonImperfect_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Mock.Setup(r => r.ThirdPersonImperfect).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }
    #endregion

    #region AuxiliaryVerb
    [Theory]
    [InlineData(AuxiliaryVerb.Haben)]
    [InlineData(AuxiliaryVerb.Sein)]
    public void AuxiliaryVerb_ShouldNotHaveValidationError_WhenNotNull_AndValidEnum(AuxiliaryVerb value)
    {
        Mock.Setup(r => r.AuxiliaryVerb).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Fact]
    public void AuxiliaryVerb_ShouldHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.AuxiliaryVerb).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Fact]
    public void AuxiliaryVerb_ShouldHaveValidationError_InvalidValue()
    {
        Mock.Setup(r => r.AuxiliaryVerb).Returns((AuxiliaryVerb)99);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.Perfect).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.Perfect);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Perfect_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Mock.Setup(r => r.Perfect).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.Separability).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.Separability);
    }

    [Fact]
    public void Separability_ShouldHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.Separability).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.Transitivity).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.Transitivity);
    }

    [Fact]
    public void Transitivity_ShouldHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.Transitivity).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.Gender).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.Gender);
    }

    [Fact]
    public void Gender_ShouldNotHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.Gender).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.Plural).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.Plural);
    }

    [Fact]
    public void Plural_ShouldNotHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.Plural).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.Preposition).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.Preposition);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Preposition_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Mock.Setup(r => r.Preposition).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.Preposition);
    }
    #endregion

    #region PrepositionCase
    [Fact]
    public void PrepositionCase_ShouldNotHaveValidationError_WhenNull_AndPrepositionCaseNull()
    {
        Mock.Setup(r => r.Preposition).Returns(() => null);
        Mock.Setup(r => r.PrepositionCase).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.PrepositionCase);
    }

    [Theory]
    [InlineData(StringData.Whitespace)]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void PrepositionCase_ShouldHaveValidationError_WhenNull_AndPrepositionNotNull(string? prepositionValue)
    {
        Mock.Setup(r => r.Preposition).Returns(() => prepositionValue);
        Mock.Setup(r => r.PrepositionCase).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.PrepositionCase);
    }
    #endregion

    #region Modifiers
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.Whitespace)]
    [InlineData(StringData.CharString25)]
    public void Comparative_ShouldHaveValidationError_WhenNotNull(string? value)
    {
        Mock.Setup(r => r.Comparative).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.Comparative);
    }

    [Fact]
    public void Comparative_ShouldNotHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.Comparative).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.Comparative);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.Whitespace)]
    [InlineData(StringData.CharString25)]
    public void Superlative_ShouldHaveValidationError_WhenNotNull(string? value)
    {
        Mock.Setup(r => r.Superlative).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.Superlative);
    }

    [Fact]
    public void Superlative_ShouldNotHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.Superlative).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
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
        Mock.Setup(r => r.FixedPlurality).Returns(value);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldHaveValidationErrorFor(request => request.FixedPlurality);
    }

    [Fact]
    public void FixedPlurality_ShouldNotHaveValidationError_WhenNull()
    {
        Mock.Setup(r => r.FixedPlurality).Returns(() => null);
        var result = Validator.TestValidate(Mock.Object);
        result.ShouldNotHaveValidationErrorFor(request => request.FixedPlurality);
    }
    #endregion
}
