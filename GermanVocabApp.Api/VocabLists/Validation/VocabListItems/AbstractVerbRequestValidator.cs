using FluentValidation;
using Osiris.FluentValidation;
using GermanVocabApp.Shared.Validation;
using GermanVocabApp.Api.VocabLists.Contracts;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabListItems;

public abstract class AbstractVerbRequestValidator<TRequest> : AbstractListItemRequestValidator<TRequest>
    where TRequest : IListItemRequest
{
    protected override void ConfigureNullabilityRules()
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

    protected override void ConfigureStringLengthRules()
    {
        base.ConfigureStringLengthRules();
        RuleFor(v => v.ThirdPersonPresent).StringLengthRange(VerbValidationData.VerbMinLength,
                                                             VerbValidationData.VerbMaxLength);
        RuleFor(v => v.ThirdPersonImperfect).StringLengthRange(VerbValidationData.VerbMinLength,
                                                             VerbValidationData.VerbMaxLength);
        RuleFor(v => v.Perfect).StringLengthRange(VerbValidationData.VerbMinLength,
                                                  VerbValidationData.VerbMaxLength);

        RuleFor(v => v.Preposition).StringLengthRange(ListItemValidationData.PrepositionMinLength,
                                                      ListItemValidationData.PluralMaxLength);
    }
}
