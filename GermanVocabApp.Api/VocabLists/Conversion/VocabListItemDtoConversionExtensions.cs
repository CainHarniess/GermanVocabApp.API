using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class VocabListItemDtoConversionExtensions
{
    public static IEnumerable<VocabListItemResponse> ToResponses(this IEnumerable<VocabListItemDto> dtos)
    {
        return dtos.Select(dto => dto.ToResponse());
    }

    public static VocabListItemResponse ToResponse(this VocabListItemDto dto)
    {
        return new VocabListItemResponse()
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
            VocabListId = dto.VocabListId,
            FixedPlurality = dto.FixedPlurality,
        };
    }

    public static IEnumerable<UpdateVocabListItemRequest> ToUpdateRequests(this IEnumerable<VocabListItemDto> dtos)
    {
        return dtos.Select(dto => dto.ToUpdateRequest());
    }

    public static UpdateVocabListItemRequest ToUpdateRequest(this VocabListItemDto dto)
    {
        return new UpdateVocabListItemRequest()
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