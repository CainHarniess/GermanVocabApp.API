using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion.Items;

internal static class ItemRequestCreationConversionExtensions
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
}