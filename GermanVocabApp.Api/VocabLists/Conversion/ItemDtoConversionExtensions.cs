using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class ItemDtoConversionExtensions
{
    public static IEnumerable<ItemResponse> ToResponses(this IEnumerable<VocabListItemDto> dtos)
    {
        return dtos.Select(dto => dto.ToResponse());
    }

    internal static ItemResponse ToResponse(this VocabListItemDto dto)
    {
        if (!dto.Id.HasValue)
        {
            throw new UnexpectedNullIdException("Expect non-null list item ID when copying to response object.");
        }
        if (!dto.VocabListId.HasValue)
        {
            throw new UnexpectedNullIdException("Expect list item to have non-null list ID when copying to response object.");
        }
        return new ItemResponse()
        {
            Id = dto.Id.Value,
            WordType = dto.WordType,
            IsWeakMasculineNoun = dto.IsWeakMasculineNoun,
            ReflexiveCase = dto.ReflexiveCase,
            Separability = dto.Separability,
            Transitivity = dto.Transitivity,
            ThirdPersonPresent = dto.ThirdPersonPresent,
            ThirdPersonImperfect = dto.ThirdPersonImperfect,
            AuxiliaryVerb = dto.AuxiliaryVerb,
            Perfect = dto.Perfect,
            Gender = dto.Gender,
            German = dto.German,
            Plural = dto.Plural,
            Preposition = dto.Preposition,
            PrepositionCase = dto.PrepositionCase,
            Comparative = dto.Comparative,
            Superlative = dto.Superlative,
            English = dto.English,
            VocabListId = dto.VocabListId.Value,
            FixedPlurality = dto.FixedPlurality,
        };
    }

    public static IEnumerable<ItemRequest> ToUpdateRequests(this IEnumerable<VocabListItemDto> dtos)
    {
        return dtos.Select(dto => dto.ToRequest());
    }

    public static ItemRequest ToRequest(this VocabListItemDto dto)
    {
        return new ItemRequest()
        {
            Id = dto.Id,
            WordType = dto.WordType,
            IsWeakMasculineNoun = dto.IsWeakMasculineNoun,
            ReflexiveCase = dto.ReflexiveCase,
            Separability = dto.Separability,
            Transitivity = dto.Transitivity,
            ThirdPersonPresent = dto.ThirdPersonPresent,
            ThirdPersonImperfect = dto.ThirdPersonImperfect,
            AuxiliaryVerb = dto.AuxiliaryVerb,
            Perfect = dto.Perfect,
            Gender = dto.Gender,
            German = dto.German,
            Plural = dto.Plural,
            Preposition = dto.Preposition,
            PrepositionCase = dto.PrepositionCase,
            Comparative = dto.Comparative,
            Superlative = dto.Superlative,
            English = dto.English,
            FixedPlurality = dto.FixedPlurality,
        };
    }
}