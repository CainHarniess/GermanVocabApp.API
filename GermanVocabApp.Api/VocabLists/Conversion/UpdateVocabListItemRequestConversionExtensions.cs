using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class UpdateVocabListItemRequestConversionExtensions
{
    public static IEnumerable<UpdateVocabListItemDto> ToDtos(this IEnumerable<UpdateVocabListItemRequest> dtos,
        Guid listId)
    {
        return dtos.Select(dto => dto.ToDto(listId));
    }

    public static UpdateVocabListItemDto ToDto(this UpdateVocabListItemRequest updateRequest, Guid listId)
    {
        return new UpdateVocabListItemDto()
        {
            Id = updateRequest.Id,
            WordType = updateRequest.WordType,
            IsWeakMasculineNoun = updateRequest.IsWeakMasculineNoun,
            ReflexiveCase = updateRequest.ReflexiveCase,
            Separability = updateRequest.Separability,
            Transitivity = updateRequest.Transitivity,
            ThirdPersonPresent = updateRequest.ThirdPersonPresent,
            ThirdPersonImperfect = updateRequest.ThirdPersonImperfect,
            AuxiliaryVerb = updateRequest.AuxiliaryVerb,
            Perfect = updateRequest.Perfect,
            Gender = updateRequest.Gender,
            German = updateRequest.German,
            Plural = updateRequest.Plural,
            Preposition = updateRequest.Preposition,
            PrepositionCase = updateRequest.PrepositionCase,
            Comparative = updateRequest.Comparative,
            Superlative = updateRequest.Superlative,
            English = updateRequest.English,
            VocabListId = listId,
            FixedPlurality = updateRequest.FixedPlurality,
        };
    }
}