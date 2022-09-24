using FluentValidation;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Shared.Validation;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

public abstract class AbstractModifierRequestValidator<TModifierRequest>
    : AbstractListItemRequestValidator<TModifierRequest>
    where TModifierRequest : IListItemRequest
{
    protected override void ConfigureNullabilityRules()
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
    }

    protected override void ConfigureStringLengthRules()
    {
        base.ConfigureStringLengthRules();
        RuleFor(m => m.Comparative).StringLengthRange(ModifierValidationData.ComparativeMinLength,
                                                      ModifierValidationData.ComparativeMaxLength);
        RuleFor(m => m.Superlative).StringLengthRange(ModifierValidationData.SuperlativeMinLength,
                                                      ModifierValidationData.SuperlativeMaxLength);
    }
}
