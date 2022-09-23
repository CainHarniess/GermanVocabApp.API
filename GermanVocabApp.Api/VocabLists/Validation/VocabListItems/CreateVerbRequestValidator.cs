using FluentValidation;
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
        RuleFor(v => v.Separability).NotNull();
        RuleFor(v => v.Transitivity).NotNull();
        RuleFor(v => v.Gender).Null();
        RuleFor(v => v.Comparative).Null();
        RuleFor(v => v.Superlative).Null();
        RuleFor(v => v.FixedPlurality).Null();
    }

    private void ConfigureStringLengthRules()
    {
        RuleFor(v => v.ThirdPersonPresent).MinimumLength(VocabListItemValidationData.VerbMinLength)
                                          .MaximumLength(VocabListItemValidationData.VerbMaxLength);
    }
}