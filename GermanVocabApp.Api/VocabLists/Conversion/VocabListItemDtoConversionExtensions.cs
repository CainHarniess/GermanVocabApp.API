using GermanVocabApp.Api.VocabLists.Models;
using VocabListItemDto = GermanVocabApp.DataAccess.Shared.DataTransfer.VocabListItemDto;

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
            IsSeparable = dto.Separability,
            IsTransitive = dto.Transitivity,
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
        };
    }
}