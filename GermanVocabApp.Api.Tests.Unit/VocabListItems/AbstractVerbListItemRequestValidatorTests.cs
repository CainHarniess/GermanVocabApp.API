using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public abstract class AbstractVerbListItemRequestValidatorTests<TVerbValidator, TVerbRequest> 
    : AbstractListItemRequestValidatorTests<TVerbValidator, TVerbRequest>
    where TVerbValidator : AbstractListItemRequestValidator<TVerbRequest>
    where TVerbRequest : IListItemRequest
{
    protected AbstractVerbListItemRequestValidatorTests() : base()
    {
        Request.WordType = WordType.Verb;
    }

    #region IsWeakMasculineNoun
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsWeakMasculineNoun_ShouldHaveValidationError_WhenNotNull(bool value)
    {
        Request.IsWeakMasculineNoun = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }

    [Fact]
    public void IsWeakMasculineNoun_ShouldNotHaveValidationError_WhenNull()
    {
        Request.IsWeakMasculineNoun = null;
        var result = Validator.TestValidate(Request);
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
        Request.ReflexiveCase = value;
        var result = Validator.TestValidate(Request);
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
        Request.ThirdPersonPresent = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void ThirdPersonPresent_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Request.ThirdPersonPresent = value;
        var result = Validator.TestValidate(Request);
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
        Request.ThirdPersonImperfect = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void ThirdPersonImperfect_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Request.ThirdPersonImperfect = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }
    #endregion

    #region AuxiliaryVerb
    [Theory]
    [InlineData(AuxiliaryVerb.Haben)]
    [InlineData(AuxiliaryVerb.Sein)]
    public void AuxiliaryVerb_ShouldNotHaveValidationError_WhenNotNull_AndValidEnum(AuxiliaryVerb value)
    {
        Request.AuxiliaryVerb = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Fact]
    public void AuxiliaryVerb_ShouldHaveValidationError_WhenNull()
    {
        Request.AuxiliaryVerb = null;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Fact]
    public void AuxiliaryVerb_ShouldHaveValidationError_InvalidValue()
    {
        Request.AuxiliaryVerb = (AuxiliaryVerb)99;
        var result = Validator.TestValidate(Request);
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
        Request.Perfect = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Perfect);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Perfect_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Request.Perfect = value;
        var result = Validator.TestValidate(Request);
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
        Request.Separability = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Separability);
    }

    [Fact]
    public void Separability_ShouldHaveValidationError_WhenNull()
    {
        Request.Separability = null;
        var result = Validator.TestValidate(Request);
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
        Request.Transitivity = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Transitivity);
    }

    [Fact]
    public void Transitivity_ShouldHaveValidationError_WhenNull()
    {
        Request.Transitivity = null;
        var result = Validator.TestValidate(Request);
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
        Request.Gender = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Gender);
    }

    [Fact]
    public void Gender_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Gender = null;
        var result = Validator.TestValidate(Request);
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
        Request.Plural = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Plural);
    }

    [Fact]
    public void Plural_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Plural = null;
        var result = Validator.TestValidate(Request);
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
        Request.Preposition = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Preposition);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Preposition_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Request.Preposition = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Preposition);
    }
    #endregion

    #region PrepositionCase

    [Theory(Skip = "Not yet implemented")]
    [InlineData(null)]
    public void PrepositionCase_ShouldNotHaveValidationError_WhenNull_AndPrepositionCaseNull(string? value)
    {

    }

    [Theory(Skip = "Not yet implemented")]
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
        Request.Comparative = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Comparative);
    }

    [Fact]
    public void Comparative_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Comparative = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Comparative);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.Whitespace)]
    [InlineData(StringData.CharString25)]
    public void Superlative_ShouldHaveValidationError_WhenNotNull(string? value)
    {
        Request.Superlative = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Superlative);
    }

    [Fact]
    public void Superlative_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Superlative = null;
        var result = Validator.TestValidate(Request);
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
        Request.FixedPlurality = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.FixedPlurality);
    }

    [Fact]
    public void FixedPlurality_ShouldNotHaveValidationError_WhenNull()
    {
        Request.FixedPlurality = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.FixedPlurality);
    }
    #endregion
}
