using FluentValidation;
using Osiris.FluentValidation;
using GermanVocabApp.Shared.Validation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

public class CreateVerbRequestValidator : CreateVocabListItemRequestValidator
{
    public CreateVerbRequestValidator()
        : base()
    {
        ConfigureNullabilityRules();
        ConfigureStringLengthRules();
    }

    private void ConfigureNullabilityRules()
    {
        RuleFor(v => v.IsWeakMasculineNoun).Null();
        RuleFor(v => v.Separability).NotNull().IsInEnum();
        RuleFor(v => v.Transitivity).NotNull().IsInEnum();
        RuleFor(v => v.AuxiliaryVerb).NotNull().IsInEnum();
        RuleFor(v => v.Gender).Null();
        RuleFor(v => v.Plural).Null();
        RuleFor(v => v.Comparative).Null();
        RuleFor(v => v.Superlative).Null();
        RuleFor(v => v.FixedPlurality).Null();
    }

    private void ConfigureStringLengthRules()
    {
        RuleFor(v => v.ThirdPersonPresent).StringLengthRange(VerbValidationData.VerbMinLength,
                                                             VerbValidationData.VerbMaxLength);
        RuleFor(v => v.ThirdPersonImperfect).StringLengthRange(VerbValidationData.VerbMinLength,
                                                             VerbValidationData.VerbMaxLength);
        RuleFor(v => v.Perfect).StringLengthRange(VerbValidationData.VerbMinLength,
                                                  VerbValidationData.VerbMaxLength);

        RuleFor(v => v.Preposition).StringLengthRange(ListItemValidationData.PrepositionMinLength,
                                                      ListItemValidationData.PrepositionMaxLength);
    }
}