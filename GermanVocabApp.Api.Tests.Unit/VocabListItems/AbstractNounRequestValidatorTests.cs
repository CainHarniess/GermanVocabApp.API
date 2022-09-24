using FluentValidation.TestHelper;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Api.VocabLists.Validation.VocabListItems;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.Tests.Unit.VocabListItems;

public abstract class AbstractNounRequestValidatorTests<TNounValidator, TNounRequest>
    : AbstractListItemRequestValidatorTests<TNounValidator, TNounRequest>
    where TNounValidator : AbstractListItemRequestValidator<TNounRequest>
    where TNounRequest : IListItemRequest
{
    protected AbstractNounRequestValidatorTests() : base()
    {
        Request.WordType = WordType.Noun;
    }

    #region IsWeakmasculineNoun
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsWeakMasculineNoun_ShouldHaveValidationError_WhenNotNull(bool value)
    {
        Request.IsWeakMasculineNoun = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }

    [Theory]
    [InlineData(null)]
    public void IsWeakMasculineNoun_ShouldHaveValidationError_WhenNull(bool? value)
    {
        Request.IsWeakMasculineNoun = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.IsWeakMasculineNoun);
    }
    #endregion

    #region ReflexiveCase
    [Fact]
    public void ReflexiveCase_ShouldNotHaveValidationError_WhenNull()
    {
        Request.ReflexiveCase = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.ReflexiveCase);
    }

    [Theory]
    [InlineData(ReflexiveCase.Accusative)]
    [InlineData(ReflexiveCase.Dative)]
    public void ReflexiveCase_ShouldHaveValidationError_WhenNotNull(ReflexiveCase value)
    {
        Request.ReflexiveCase = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.ReflexiveCase);
    }
    #endregion

    #region Separability
    [Fact]
    public void Separability_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Separability = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Separability);
    }

    [Theory]
    [InlineData(Separability.None)]
    [InlineData(Separability.Separable)]
    [InlineData(Separability.Inseparable)]
    public void Separability_ShouldHaveValidationError_WhenNotNull(Separability value)
    {
        Request.Separability = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Separability);
    }
    #endregion

    #region Transitivity
    [Fact]
    public void Transitivity_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Transitivity = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Transitivity);
    }

    [Theory]
    [InlineData(Transitivity.Transitive)]
    [InlineData(Transitivity.Intransitive)]
    [InlineData(Transitivity.Both)]
    public void Transitivity_ShouldHaveValidationError_WhenNotNull(Transitivity value)
    {
        Request.Transitivity = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Transitivity);
    }
    #endregion

    #region ThidPersonPresent
    [Fact]
    public void ThirdPersonPresent_ShouldNotHaveValidationError_WhenNull()
    {
        Request.ThirdPersonPresent = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void ThirdPersonPresent_ShouldHaveValidationError_WhenNotNull(string value)
    {
        Request.ThirdPersonPresent = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonPresent);
    }
    #endregion

    #region ThirdPersonImperfect
    [Fact]
    public void ThirdPersonImperfect_ShouldNotHaveValidationError_WhenNull()
    {
        Request.ThirdPersonImperfect = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void ThirdPersonImperfect_ShouldHaveValidationError_WhenNotNull(string value)
    {
        Request.ThirdPersonImperfect = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.ThirdPersonImperfect);
    }
    #endregion

    #region AuxiliaryVerb
    [Fact]
    public void AuxiliaryVerb_ShouldNotHaveValidationError_WhenNull()
    {
        Request.AuxiliaryVerb = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }

    [Theory]
    [InlineData(AuxiliaryVerb.Haben)]
    [InlineData(AuxiliaryVerb.Sein)]
    public void AuxiliaryVerb_ShouldHaveValidationError_WhenNotNull(AuxiliaryVerb value)
    {
        Request.AuxiliaryVerb = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.AuxiliaryVerb);
    }
    #endregion

    #region Perfect
    [Fact]
    public void Perfect_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Perfect = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Perfect);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void Perfect_ShouldHaveValidationError_WhenNotNull(string value)
    {
        Request.Perfect = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Perfect);
    }
    #endregion

    #region Gender
    [Fact]
    public void Gender_ShouldHaveValidationError_WhenNull()
    {
        Request.Gender = null;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Gender);
    }

    [Theory]
    [InlineData(Gender.Masculine)]
    [InlineData(Gender.Feminine)]
    [InlineData(Gender.Neuter)]
    public void Gender_ShouldNotHaveValidationError_WhenNotNull(Gender value)
    {
        Request.Gender = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Gender);
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

    #region Plural
    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    [InlineData(StringData.CharString2)]
    [InlineData(StringData.CharString26)]
    public void Plural_ShouldHaveValidationError_WhenInvalidLength(string value)
    {
        Request.Plural = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Plural);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    [InlineData(StringData.CharString25)]
    public void Plural_ShouldNotHaveValidationError_WhenNull_OrValidLength(string? value)
    {
        Request.Plural = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Plural);
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

    #region Comparative
    [Fact]
    public void Comparative_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Comparative = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Comparative);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void Comparative_ShouldHaveValidationError_WhenNotNull(string value)
    {
        Request.Comparative = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Comparative);
    }
    #endregion

    #region Superlative
    [Fact]
    public void Superlative_ShouldNotHaveValidationError_WhenNull()
    {
        Request.Superlative = null;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.Superlative);
    }

    [Theory]
    [InlineData(StringData.Empty)]
    [InlineData(StringData.CharString1)]
    public void Superlative_ShouldHaveValidationError_WhenNotNull(string value)
    {
        Request.Superlative = value;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.Superlative);
    } 
    #endregion

    #region FixedPlurality
    [Fact]
    public void FixedPlurality_ShouldHaveValidationError_WhenNull()
    {
        Request.FixedPlurality = null;
        var result = Validator.TestValidate(Request);
        result.ShouldHaveValidationErrorFor(request => request.FixedPlurality);
    }

    [Theory]
    [InlineData(FixedPlurality.None)]
    [InlineData(FixedPlurality.Singular)]
    [InlineData(FixedPlurality.Plural)]
    public void FixedPlurality_ShouldNotHaveValidationError_WhenNotNull(FixedPlurality value)
    {
        Request.FixedPlurality = value;
        var result = Validator.TestValidate(Request);
        result.ShouldNotHaveValidationErrorFor(request => request.FixedPlurality);
    }
    #endregion
}