using FluentValidation;

namespace GermanVocabApp.Api.FluentValidation.Words;

public class VerbRequestValidator : WordRequestValidator
{
    public VerbRequestValidator() : base()
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
}
