using FluentValidation;
using GermanVocabApp.Shared.Constraints;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.FluentValidation.Validators;

internal class FluentModifierValidator : FluentWordValidator
{
    public FluentModifierValidator() : base()
    {
        RuleFor(m => m.IsWeakMasculineNoun).Null();
        RuleFor(m => m.ReflexiveCase).Null();
        RuleFor(m => m.Separability).Null();
        RuleFor(m => m.Transitivity).Null();
        RuleFor(m => m.ThirdPersonPresent).Null();
        RuleFor(m => m.ThirdPersonImperfect).Null();
        RuleFor(m => m.AuxiliaryVerb).Null();
        RuleFor(m => m.Perfect).Null();
        RuleFor(m => m.Gender).Null();
        RuleFor(m => m.Plural).Null();
        RuleFor(m => m.Preposition).Null();
        RuleFor(m => m.PrepositionCase).Null();
        RuleFor(m => m.FixedPlurality).Null();

        RuleFor(m => m.Comparative).StringLengthRange(ModifierConstraints.ComparativeMinLength,
                                                      ModifierConstraints.ComparativeMaxLength);
        RuleFor(m => m.Superlative).StringLengthRange(ModifierConstraints.SuperlativeMinLength,
                                                      ModifierConstraints.SuperlativeMaxLength);
    }
}