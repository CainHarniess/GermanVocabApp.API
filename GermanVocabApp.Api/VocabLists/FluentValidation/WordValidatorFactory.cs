﻿using FluentValidation;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.FluentValidation;

public class WordValidatorFactory : IFactory<IValidator<ItemRequest>, ItemRequest>
{
    private Dictionary<WordType, FluentWordValidator> _validators;

    public WordValidatorFactory(FluentNounValidator nounValidator, FluentVerbValidator verbValidator,
                                FluentModifierValidator modifierValidator)
    {
        _validators = new Dictionary<WordType, FluentWordValidator>()
        {
            { WordType.Noun, nounValidator },
            { WordType.Verb, verbValidator },
            { WordType.Adjective, modifierValidator },
            { WordType.Adverb, modifierValidator },
        };
    }

    internal WordValidatorFactory()
    {
        FluentModifierValidator modifierValidator = new FluentModifierValidator();
        _validators = new Dictionary<WordType, FluentWordValidator>()
        {
            { WordType.Noun, new FluentNounValidator() },
            { WordType.Verb, new FluentVerbValidator() },
            { WordType.Adjective, modifierValidator },
            { WordType.Adverb, modifierValidator },
        };
    }

    public IValidator<ItemRequest> Create(ItemRequest request)
    {
        WordType wordType = request.WordType;
        if (!_validators.ContainsKey(wordType))
        {
            throw new ArgumentException($"Invalid word type {Enum.GetName(wordType)} provided with value {wordType}.");
        }
        return _validators[wordType];
    }
}
