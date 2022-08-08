using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Conversion;

public static class VocabListItemConversionExtensions
{
    public static IEnumerable<VocabListItemDto> ToDtos(this IEnumerable<VocabListItem> entities)
    {
        return entities.Select(vli => vli.ToDto());
    }

    public static VocabListItemDto ToDto(this VocabListItem entity)
    {
        return new VocabListItemDto()
        {
            Id = entity.Id,
            WordType = entity.WordType,
            ReflexiveCase = entity.ReflexiveCase,
            IsSeparable = entity.IsSeparable,
            IsTransitive = entity.IsTransitive,
            ThirdPersonPresent = entity.ThirdPersonPresent,
            ThirdPersonImperfect = entity.ThirdPersonImperfect,
            AuxiliaryVerb = entity.AuxiliaryVerb,
            Perfect = entity.Perfect,
            Gender = entity.Gender,
            German = entity.German,
            Plural = entity.Plural,
            Preposition = entity.Preposition,
            PrepositionCase = entity.PrepositionCase,
            Comparative = entity.Comparative,
            Superlative = entity.Superlative,
            English = entity.English,
            VocabListId = entity.VocabListId,
        };
    }
}
