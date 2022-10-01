﻿using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class ItemRequestConversionExtensions
{
    public static IEnumerable<VocabListItemDto> ToCreationDtos(this IEnumerable<ItemRequest> dtos)
    {
        return dtos.Select(dto => dto.ToCreationDto());
    }

    public static VocabListItemDto ToCreationDto(this ItemRequest request)
    {
        return new VocabListItemDto()
        {
            Id = null,
            WordType = request.WordType,
            IsWeakMasculineNoun = request.IsWeakMasculineNoun,
            ReflexiveCase = request.ReflexiveCase,
            Separability = request.Separability,
            Transitivity = request.Transitivity,
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
            VocabListId = null,
            FixedPlurality = request.FixedPlurality,
        };
    }

    public static IEnumerable<VocabListItemDto> ToUpdateDtos(this IEnumerable<ItemRequest> dtos,
        Guid? listId)
    {
        return dtos.Select(dto => dto.ToUpdateDto(listId));
    }

    public static VocabListItemDto ToUpdateDto(this ItemRequest request, Guid? listId)
    {
        return new VocabListItemDto()
        {
            Id = request.Id,
            WordType = request.WordType,
            IsWeakMasculineNoun = request.IsWeakMasculineNoun,
            ReflexiveCase = request.ReflexiveCase,
            Separability = request.Separability,
            Transitivity = request.Transitivity,
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
            VocabListId = listId,
            FixedPlurality = request.FixedPlurality,
        };
    }
}