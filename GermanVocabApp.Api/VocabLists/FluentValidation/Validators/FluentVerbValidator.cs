﻿using FluentValidation;
using GermanVocabApp.Shared.Constraints;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.Validators;

public class FluentVerbValidator : FluentWordValidator
{
    public FluentVerbValidator() : base()
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

        RuleFor(v => v.ThirdPersonPresent).StringLengthRange(VerbConstraints.VerbMinLength,
                                                             VerbConstraints.VerbMaxLength);
        RuleFor(v => v.ThirdPersonImperfect).StringLengthRange(VerbConstraints.VerbMinLength,
                                                             VerbConstraints.VerbMaxLength);
        RuleFor(v => v.Perfect).StringLengthRange(VerbConstraints.VerbMinLength,
                                                  VerbConstraints.VerbMaxLength);

        RuleFor(v => v.Preposition).StringLengthRange(ListItemConstraints.PrepositionMinLength,
                                                      ListItemConstraints.PluralMaxLength);

        this.NullIfElseNotNull(n => n.Preposition == null, n => n.PrepositionCase);
    }
}
