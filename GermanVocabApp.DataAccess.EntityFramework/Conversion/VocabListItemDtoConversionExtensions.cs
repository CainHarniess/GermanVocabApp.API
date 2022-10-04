using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Conversion;

internal static class VocabListItemDtoConversionExtensions
{
    public static VocabListItem[] ToEntities(this VocabListItemDto[] dtos,
                                             DateTime creationTimeStamp, Guid vocabListId)
    {
        return dtos.Select(dto => dto.ToEntity(creationTimeStamp, vocabListId))
                   .ToArray();
    }

    public static IEnumerable<VocabListItem> ToEntities(this IEnumerable<VocabListItemDto> dtos,
                                                        DateTime creationTimeStamp)
    {
        return dtos.Select(dto => dto.ToEntity(creationTimeStamp));
    }

    public static VocabListItem ToEntity(this VocabListItemDto dto, DateTime creationTimeStamp,
                                         Guid vocabListId)
    {
        VocabListItem entity = dto.ToEntity(creationTimeStamp);
        entity.VocabListId = vocabListId;
        return entity;
    }

    public static VocabListItem ToEntity(this VocabListItemDto dto, DateTime creationTimeStamp)
    {
        VocabListItem entity =  new VocabListItem()
        {
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
            CreatedDate = creationTimeStamp,
            UpdatedDate = null,
            DeletedDate = null,
        };
        if (dto.Id.HasValue)
        {
            entity.Id = dto.Id.Value;
        }
        return entity;
    }

    public static void CopyTo(this VocabListItemDto dto,
                              VocabListItem entity, DateTime updateTimestamp)
    {
        if (!dto.VocabListId.HasValue)
        {
            throw new UnexpectedNullIdException("Expect list item dto to have non-null list ID when copying to entity.");
        }

        entity.WordType = dto.WordType;
        entity.IsWeakMasculineNoun = dto.IsWeakMasculineNoun;
        entity.ReflexiveCase = dto.ReflexiveCase;
        entity.Separability = dto.Separability;
        entity.Transitivity = dto.Transitivity;
        entity.ThirdPersonPresent = dto.ThirdPersonPresent;
        entity.ThirdPersonImperfect = dto.ThirdPersonImperfect;
        entity.AuxiliaryVerb = dto.AuxiliaryVerb;
        entity.Perfect = dto.Perfect;
        entity.Gender = dto.Gender;
        entity.German = dto.German;
        entity.Plural = dto.Plural;
        entity.Preposition = dto.Preposition;
        entity.PrepositionCase = dto.PrepositionCase;
        entity.Comparative = dto.Comparative;
        entity.Superlative = dto.Superlative;
        entity.English = dto.English;
        entity.VocabListId = dto.VocabListId.Value;
        entity.FixedPlurality = dto.FixedPlurality;
        entity.UpdatedDate = updateTimestamp;
    }
}
