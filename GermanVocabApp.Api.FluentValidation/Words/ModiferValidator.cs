﻿using FluentValidation;

namespace GermanVocabApp.Api.FluentValidation.Words;

public class ModiferValidator : WordRequestValidator
{
    public ModiferValidator() : base()
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
}