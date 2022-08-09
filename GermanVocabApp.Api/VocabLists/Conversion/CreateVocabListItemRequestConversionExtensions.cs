﻿using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class CreateVocabListItemRequestConversionExtensions
{
    public static CreateVocabListItemDto ToDto(this CreateVocabListItemRequest request)
    {
        return new CreateVocabListItemDto()
        {
            WordType = request.WordType,
            ReflexiveCase = request.ReflexiveCase,
            IsSeparable = request.IsSeparable,
            IsTransitive = request.IsTransitive,
            ThirdPersonPresent = request.ThirdPersonPresent,
            ThirdPersonImperfect = request.ThirdPersonImperfect,
            AuxiliaryVerb = request.AuxiliaryVerb,
            Perfect = request.Perfect,
            Gender = request.Gender,
            German = request.German,
            Plural = request.Plural,
            Preposition = request.Preposition,
            PrepositionCase = request.PrepositionCase,
            Comparative = request.Comparative,
            Superlative = request.Superlative,
            English = request.English,
            VocabListId = request.VocabListId,
        };
    }
}