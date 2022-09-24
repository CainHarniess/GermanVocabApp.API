using FluentValidation;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Shared.Validation;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

public abstract class AbstractNounRequestValidator<TNounRequest> : AbstractListItemRequestValidator<TNounRequest>
    where TNounRequest : IListItemRequest
{
    protected override void ConfigureStringLengthRules()
    {
        base.ConfigureStringLengthRules();
        RuleFor(n => n.Preposition).StringLengthRange(ListItemValidationData.PrepositionMinLength, ListItemValidationData.PluralMaxLength);
        RuleFor(n => n.Plural).StringLengthRange(NounValidationData.PluralMinLength, NounValidationData.PluralMaxLength);
    }

    protected override void ConfigureNullabilityRules()
    {
        RuleFor(n => n.IsWeakMasculineNoun).NotNull();
        RuleFor(n => n.ReflexiveCase).Null();
        RuleFor(n => n.Separability).Null();
        RuleFor(n => n.Transitivity).Null();
        RuleFor(n => n.ThirdPersonPresent).Null();
        RuleFor(n => n.ThirdPersonImperfect).Null();
        RuleFor(n => n.AuxiliaryVerb).Null();
        RuleFor(n => n.Perfect).Null();
        RuleFor(n => n.Gender).NotNull();
        RuleFor(n => n.Comparative).Null();
        RuleFor(n => n.Superlative).Null();
        RuleFor(n => n.FixedPlurality).NotNull();
    }
}
