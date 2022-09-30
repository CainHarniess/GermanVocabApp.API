using FluentValidation;
using GermanVocabApp.Shared.Constraints;
using GermanVocabApp.Shared.Data;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.FluentValidation.Validators;

public class FluentNounValidator : FluentWordValidator
{
    public FluentNounValidator() : base()
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

        RuleFor(n => n.Preposition).StringLengthRange(ListItemConstraints.PrepositionMinLength, ListItemConstraints.PluralMaxLength);
        RuleFor(n => n.Plural).StringLengthRange(NounConstraints.PluralMinLength, NounConstraints.PluralMaxLength);

        this.NullIfElseNotNull(n => n.Preposition == null, n => n.PrepositionCase);
        this.NotNullIf(n => n.FixedPlurality == FixedPlurality.Plural, n => n.Plural);
    }
}
