using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Conversion;

internal static class CreateVocabListItemDtoConversionExtensions
{
    public static IEnumerable<VocabListItem> ToEntities(this IEnumerable<CreateVocabListItemDto> dtos,
                                                        DateTime creationTimeStamp, Guid vocabListId)
    {
        return dtos.Select(dto => dto.ToEntity(creationTimeStamp, vocabListId));
    }

    public static IEnumerable<VocabListItem> ToEntities(this IEnumerable<CreateVocabListItemDto> dtos,
                                                        DateTime creationTimeStamp)
    {
        return dtos.Select(dto => dto.ToEntity(creationTimeStamp));
    }

    public static VocabListItem ToEntity(this CreateVocabListItemDto dto, DateTime creationTimeStamp,
                                         Guid vocabListId)
    {
        VocabListItem entity = dto.ToEntity(creationTimeStamp);
        entity.VocabListId = vocabListId;
        return entity;
    }

    public static VocabListItem ToEntity(this CreateVocabListItemDto dto, DateTime creationTimeStamp)
    {
        return new VocabListItem()
        {
            WordType = dto.WordType,
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
            CreatedDate = creationTimeStamp,
            UpdatedDate = null,
            DeletedDate = null,
        };
    }
}
