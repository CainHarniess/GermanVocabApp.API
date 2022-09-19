using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

internal static class CreateVocabListItemRequestConversionExtensions
{
    public static CreateVocabListItemDto ToDto(this CreateVocabListItemRequest request)
    {
        return new CreateVocabListItemDto()
        {
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
            FixedPlurality = request.FixedPlurality,
        };
    }
}
