﻿using FluentValidation;
using GermanVocabApp.Shared.Constraints;
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

        RuleFor(n => n.PrepositionCase).NotNull().When(n => n.Preposition != null);
        RuleFor(n => n.PrepositionCase).Null().When(n => n.Preposition == null);
    }
}
